using System.Threading.Tasks;
using AElf.Contracts.MultiToken;
using AElf.Types;
using Google.Protobuf.WellKnownTypes;
using Xunit;

namespace AElf.Contracts.BingoGameContract
{

    // This class is unit test class, and it inherit TestBase. Write your unit test code inside it.
    public class BingoGameContractTests : TestBase
    {
        private async Task InitializeTests()
        {
            await BingoGameContractStub.Initialize.SendAsync(new Empty());
            await BingoGameContractStub.Initialize.SendAsync(new Empty());
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
            await BingoGameContractStub.Register.SendAsync(new Empty());

            var amount = 1_00000000;

            var tx = await BingoGameContractStub.Play.SendAsync(new PlayInput
            {
                Amount = amount,
                Type = BingoType.Small
            });

            return tx.TransactionResult.TransactionId;
        }
    }
}