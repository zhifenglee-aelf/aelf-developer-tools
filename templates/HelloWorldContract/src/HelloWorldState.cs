using AElf.Sdk.CSharp.State;

namespace AElf.Contracts.HelloWorld
{
    // The state class is used to communicate with the blockchain
    public class HelloWorldState : ContractState 
    {
        // A state that holds string value
        public StringState Message { get; set; }
    }
}