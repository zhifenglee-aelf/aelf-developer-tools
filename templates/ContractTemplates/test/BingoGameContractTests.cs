using System.Threading.Tasks;
using AElf.Contracts.MultiToken;
using AElf.Types;
using Google.Protobuf.WellKnownTypes;
using Shouldly;
using Xunit;

namespace AElf.Contracts.BingoGameContract
{

    public class BingoGameContractTests : TestBase
    {
        public async Task InitializeTests()
        {
            await BingoGameContractStub.Initialize.SendAsync(new Empty());
            await BingoGameContractStub.Initialize.SendAsync(new Empty());
        }
        [Fact]
        public async Task RegisterTests()
        {
            await InitializeTests();
            await BingoGameContractStub.Register.SendAsync(new Empty());
            var information = await BingoGameContractStub.GetPlayerInformation.CallAsync(DefaultAddress);
            information.Seed.ShouldNotBeNull();
            information.RegisterTime.ShouldNotBeNull();
        }
        private async Task InitializeAsync()
        {
            await TokenContractStub.Transfer.SendAsync(new TransferInput
            {
                To = ContractAddress,
                Symbol = "ELF",
                Amount = 1000_00000000
            });
            await TokenContractStub.Approve.SendAsync(new ApproveInput
            {
                Spender = ContractAddress,
                Symbol = "ELF",
                Amount = 1000_00000000
            });
        }
        [Fact]
        public async Task<Hash> PlayTests()
        {
            await InitializeTests();
            await InitializeAsync();
            await RegisterTests();

            var amount = 1_00000000;

            var tx = await BingoGameContractStub.Play.SendAsync(new PlayInput
            {
                Amount = amount,
                Type = BingoType.Small
            });
            var information = await BingoGameContractStub.GetPlayerInformation.CallAsync(DefaultAddress);

            return tx.TransactionResult.TransactionId;
        }
    }
}