using System.Diagnostics.CodeAnalysis;
using AElf.Contracts.Consensus.AEDPoS;
using AElf.Contracts.MultiToken;
using AElf.Cryptography.ECDSA;
using AElf.Testing.TestBase;
using AElf.Types;

namespace AElf.Contracts.BingoGameContract
{
    // This class is used to load the context required for unit testing.
    public class Module : ContractTestModule<BingoGameContract>
    {
        
    }
    
    // The TestBase class inherit ContractTestBase class, which is used to define and get stub classes required for unit testing.
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class TestBase : ContractTestBase<Module>
    {
        // You can get address of any contract via GetAddress method, for example:
        // internal Address ContractAddress => GetAddress(SmartContractAddressNameProvider.StringName);
        // Using the address and key to get stub, Like this:
        // TokenContractContainer.TokenContractStub stub = GetTester<TokenContractContainer.TokenContractStub>(TokenContractAddress, keyPair);
        
        internal BingoGameContractContainer.BingoGameContractStub BingoGameContractStub { get; set; }
        internal TokenContractContainer.TokenContractStub TokenContractStub { get; set; }
        internal RandomNumberAccessorContractContainer.RandomNumberAccessorContractStub RandomNumberAccessorContractStub { get; set; }
        protected ECKeyPair DefaultKeyPair => Accounts[0].KeyPair;
        protected Address DefaultAddress => Accounts[0].Address;
        protected ECKeyPair UserKeyPair => Accounts[1].KeyPair;
        protected Address UserAddress => Accounts[1].Address;

        public TestBase()
        {
            BingoGameContractStub = GetBingoGameContractStub(DefaultKeyPair);
            TokenContractStub = GetTokenContractTester(DefaultKeyPair);
            RandomNumberAccessorContractStub = GetRandomNumberAccessorContractStub(DefaultKeyPair);
        }

        internal BingoGameContractContainer.BingoGameContractStub GetBingoGameContractStub(ECKeyPair senderKeyPair)
        {
            return GetTester<BingoGameContractContainer.BingoGameContractStub>(ContractAddress, senderKeyPair);
        }

        internal TokenContractContainer.TokenContractStub GetTokenContractTester(ECKeyPair keyPair)
        {
            return GetTester<TokenContractContainer.TokenContractStub>(TokenContractAddress, keyPair);
        }

        internal RandomNumberAccessorContractContainer.RandomNumberAccessorContractStub GetRandomNumberAccessorContractStub(ECKeyPair keyPair)
        {   
            return GetTester<RandomNumberAccessorContractContainer.RandomNumberAccessorContractStub>(ConsensusContractAddress, keyPair);
        }
    }
    
}