using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AElf.Contracts.MultiToken;
using AElf.CSharp.Core;
using AElf.Sdk.CSharp;
using AElf.Types;
using Google.Protobuf.WellKnownTypes;

namespace AElf.Contracts.BingoGameContract
{
    public partial class BingoGameContract : BingoGameContractContainer.BingoGameContractBase
    {
        public override Empty Register(Empty input)
        {
            Assert(State.PlayerInformation[Context.Sender] == null, $"User {Context.Sender} already registered.");
            var information = new PlayerInformation
            {
                // The value of seed will influence user's game result in some aspects.
                Seed = Context.TransactionId,
                RegisterTime = Context.CurrentBlockTime
            };
            State.PlayerInformation[Context.Sender] = information;
            return new Empty();
        }

        public override Empty Initialize(Empty input)
        {
            if (State.Initialized.Value)
            {
                return new Empty();
            }

            State.TokenContract.Value =
                Context.GetContractAddressByName(SmartContractConstants.TokenContractSystemName);
            State.RandomNumberAccessorContract.Value =
                Context.GetContractAddressByName(SmartContractConstants.ConsensusContractSystemName);
            Assert(State.Admin.Value == null, "Already initialized.");
            State.Admin.Value = Context.Sender;
            State.MinimumBet.Value = BingoGameContractConstants.DefaultMinimumBet;
            State.MaximumBet.Value = BingoGameContractConstants.DefaultMaximumBet;

            State.Initialized.Value = true;
            return new Empty();
        }

        public override Int64Value Play(PlayInput input)
        {
            Assert(input.Amount >= State.MinimumBet.Value && input.Amount <= State.MaximumBet.Value, "Invalid bet amount.");

            Context.LogDebug(() => $"Playing with amount {input.Amount}");

            if (State.TokenContract.Value == null)
            {
                State.TokenContract.Value =
                    Context.GetContractAddressByName(SmartContractConstants.TokenContractSystemName);
            }

            State.TokenContract.TransferFrom.Send(new TransferFromInput
            {
                From = Context.Sender,
                To = Context.Self,
                Amount = input.Amount,
                Symbol = BingoGameContractConstants.CardSymbol,
                Memo = "Enjoy!"
            });

            var boutInformation = new BoutInformation
            {
                PlayBlockHeight = Context.CurrentHeight,
                Amount = input.Amount,
                Type = input.Type,
                PlayId = Context.OriginTransactionId,
                PlayTime = Context.CurrentBlockTime,
                Dices = new DiceList(),
                PlayerAddress = Context.Sender
            };
            State.BoutInformations[Context.OriginTransactionId] = boutInformation;
            return new Int64Value { Value = Context.CurrentHeight.Add(BingoGameContractConstants.BingoBlockHeight) };
        }    
        
        private List<int> GetDices(Hash hashValue)
        {
            var hexString = hashValue.ToHex();
            var dices = new List<int>();

            for (int i = 0; i < 3; i++)
            {
                var startIndex = i * 8;
                var intValue = int.Parse(hexString.Substring(startIndex, 8), NumberStyles.HexNumber);
                var dice = (intValue % 6 + 5) % 6 + 1;
                dices.Add(dice);
            }

            return dices;
        }
        
        public override BoolValue Bingo(Hash input)
        {
            Context.LogDebug(() => $"Getting game result of play id: {input.ToHex()}");

            Assert(input != null && !input.Value.IsNullOrEmpty(), "Invalid input.");

            var playerInformation = State.PlayerInformation[Context.Sender];

            Assert(playerInformation != null, $"User {Context.Sender} not registered before.");

            var boutInformation = State.BoutInformations[input];

            Assert(boutInformation != null, "Bout not found.");

            Assert(!boutInformation!.IsComplete, "Bout already finished.");

            Assert(boutInformation.PlayerAddress == Context.Sender, "Only the player can get the result.");

            var targetHeight = boutInformation.PlayBlockHeight.Add(BingoGameContractConstants.BingoBlockHeight);
            Assert(targetHeight <= Context.CurrentHeight, "Invalid target height.");

            if (State.RandomNumberAccessorContract.Value == null)
            {
                State.RandomNumberAccessorContract.Value =
                    Context.GetContractAddressByName(SmartContractConstants.ConsensusContractSystemName);
            }

            var randomHash = State.RandomNumberAccessorContract.GetRandomHash.Call(new Int64Value
            {
                Value = targetHeight
            });

            Assert(randomHash != null && !randomHash.Value.IsNullOrEmpty(),
                "Still preparing your game result, please wait for a while :)");

            var usefulHash = HashHelper.ConcatAndCompute(randomHash, playerInformation.Seed);
            // var bitArraySum = SumHash(usefulHash);
            var dices = GetDices(usefulHash);
            var diceNumSum = dices.Sum();
            var diceNumSumResult = GetDiceNumSumResult(diceNumSum);
            var isWin = GetResult(diceNumSumResult, boutInformation.Type);
            var award = isWin ? boutInformation.Amount : -boutInformation.Amount;
            var transferAmount = boutInformation.Amount.Add(award);
            if (transferAmount > 0)
            {
                State.TokenContract.Transfer.Send(new TransferInput
                {
                    Symbol = BingoGameContractConstants.CardSymbol,
                    Amount = transferAmount,
                    To = boutInformation.PlayerAddress,
                    Memo = "Thx for playing my game."
                });
            }

            boutInformation.Award = award;
            boutInformation.IsComplete = true;
            boutInformation.RandomNumber = diceNumSum;
            boutInformation.BingoBlockHeight = Context.CurrentHeight;
            boutInformation.Dices.Dices.Add(dices[0]);
            boutInformation.Dices.Dices.Add(dices[1]);
            boutInformation.Dices.Dices.Add(dices[2]);

            State.PlayerInformation[boutInformation.PlayerAddress] = playerInformation;
            State.BoutInformations[input] = boutInformation;

            return new BoolValue { Value = isWin };
        }
        
        public override Empty Quit(Empty input)
        {
            State.PlayerInformation.Remove(Context.Sender);
            return new Empty();
        }

        public override BoutInformation GetBoutInformation(GetBoutInformationInput input)
        {
            Assert(input != null, "Invalid input");
            Assert(input!.PlayId != null && !input.PlayId.Value.IsNullOrEmpty(), "Invalid playId");

            var boutInformation = State.BoutInformations[input.PlayId];

            Assert(boutInformation != null, "Bout not found.");

            return boutInformation;
        }
    }
    
}