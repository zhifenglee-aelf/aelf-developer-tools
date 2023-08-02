using System.Diagnostics.CodeAnalysis;
using AElf.Contracts.Consensus.AEDPoS;
using AElf.Contracts.MultiToken;

namespace AElf.Contracts.BingoGameContract
{
    // This class is used to import other contracts.
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public partial class BingoGameContractState
    {
        internal TokenContractContainer.TokenContractReferenceState TokenContract { get; set; }
        internal RandomNumberAccessorContractContainer.RandomNumberAccessorContractReferenceState RandomNumberAccessorContract { get; set; }
    }
}