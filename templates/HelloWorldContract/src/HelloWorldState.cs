using System.Diagnostics.CodeAnalysis;
using AElf.Sdk.CSharp.State;

namespace AElf.Contracts.HelloWorld
{
    // The state class is used to communicate with the blockchain.
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class HelloWorldState : ContractState 
    {
        // StringState is used to put the single data.
        public StringState Message { get; set; }
    }
}