using AElf.Cryptography.ECDSA;
using AElf.Testing.TestBase;

namespace AElf.Contracts.HelloWorld
{
    // This class is used to load the context required for unit testing.
    public class Module : ContractTestModule<HelloWorld>
    {
        
    }
    
    // The TestBase class inherit ContractTestBase class, which is used to define and get stub classes required for unit testing.
    public class TestBase : ContractTestBase<Module>
    {
        // You can get address of any contract via GetAddress method, for example:
        // internal Address ContractAddress => GetAddress(SmartContractAddressNameProvider.StringName);
        // Using the address and key to get stub, Like this:
        // TokenContractContainer.TokenContractStub stub = GetTester<TokenContractContainer.TokenContractStub>(TokenContractAddress, keyPair);

        // Declare stub class for unit testing.
        internal readonly HelloWorldContainer.HelloWorldStub HelloWorldStub;
        // private ECKeyPair KeyPair.
        private ECKeyPair DefaultKeyPair => Accounts[0].KeyPair;

        public TestBase()
        {
            HelloWorldStub = GetHelloWorldContractStub(DefaultKeyPair);
        }

        private HelloWorldContainer.HelloWorldStub GetHelloWorldContractStub(ECKeyPair senderKeyPair)
        {
            return GetTester<HelloWorldContainer.HelloWorldStub>(ContractAddress, senderKeyPair);
        }
    }
    
}