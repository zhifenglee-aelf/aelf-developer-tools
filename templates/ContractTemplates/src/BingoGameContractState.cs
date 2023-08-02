using System.Diagnostics.CodeAnalysis;
using AElf.Sdk.CSharp.State;
using AElf.Types;

namespace AElf.Contracts.BingoGameContract
{
    // The state class is used to communicate with the blockchain.
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public partial class BingoGameContractState : ContractState 
    {
        // MappedState is used to put the key-value pairs data.
        public MappedState<Address, PlayerInformation> PlayerInformation { get; set; }
        public MappedState<Hash, BoutInformation> BoutInformation { get; set; }
        public BoolState Initialized { get; set; }
        // SingletonState is used to put the single data.
        public SingletonState<Address> Admin { get; set; }
        public SingletonState<long> MinimumBet { get; set; }
        public SingletonState<long> MaximumBet { get; set; }
    }
}