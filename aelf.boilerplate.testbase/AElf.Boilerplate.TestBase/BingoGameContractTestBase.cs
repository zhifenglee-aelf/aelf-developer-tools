using AElf.Boilerplate.TestBase;
using AElf.Cryptography.ECDSA;
using AElf.Sdk.CSharp;

namespace AElf.Boilerplate.TestBase
{
    public class BingoGameContractTestBase<TContract> : DAppContractTestBase<BingoGameContractTestModule<TContract>>
        where TContract : CSharpSmartContractAbstract
    {
        // You can get address of any contract via GetAddress method, for example:
        // internal Address DAppContractAddress => GetAddress(DAppSmartContractAddressNameProvider.StringName);

        protected TStub GetBingoGameContractStub<TStub>(ECKeyPair senderKeyPair) where TStub:AElf.CSharp.Core.ContractStubBase, new()
        {
            return GetTester<TStub>(DAppContractAddress, senderKeyPair);
        }
    }
}