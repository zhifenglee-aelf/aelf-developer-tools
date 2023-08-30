using AElf.Cryptography.ECDSA;
using AElf.Testing.TestBase;

namespace AElf.Contracts.HelloWorld
{
    // This class is used to load the context required for unit testing
    public class Module : ContractTestModule<HelloWorld>
    {
        
    }
    
    // The TestBase class inherit ContractTestBase class, which is used to define and get stub classes required for unit testing
    public class TestBase : ContractTestBase<Module>
    {
        // Declare stub class for unit testing
        internal readonly HelloWorldContainer.HelloWorldStub HelloWorldStub;
        // A key pair that can be used to interact with the contract instance
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