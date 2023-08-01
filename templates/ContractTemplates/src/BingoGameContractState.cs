using System.Diagnostics.CodeAnalysis;
using AElf.Sdk.CSharp.State;
using AElf.Types;

namespace AElf.Contracts.BingoGameContract
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public partial class BingoGameContractState : ContractState 
    {
        public MappedState<Address, PlayerInformation> PlayerInformation { get; set; }
        public MappedState<Hash, BoutInformation> BoutInformation { get; set; }
        public BoolState Initialized { get; set; }
        public SingletonState<Address> Admin { get; set; }
        public SingletonState<long> MinimumBet { get; set; }
        public SingletonState<long> MaximumBet { get; set; }
    }
}