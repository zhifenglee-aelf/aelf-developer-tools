using System.Diagnostics.CodeAnalysis;
using AElf.Sdk.CSharp.State;
using AElf.Types;

namespace AElf.Contracts.HelloWorld
{
    // The state class is used to communicate with the blockchain.
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "PartialTypeWithSinglePart")]
    public partial class HelloWorldState : ContractState 
    {
        public StringState Message { get; set; }
        
        // SingletonState is used to put the single data.
        public SingletonState<HelloWorld> GetSingleton { get; set; }
        
        // MappedState is used to put the key-value pairs data.
        public MappedState<Address, HelloWorld> GetSingletonByAddress { get; set; }
    }
}