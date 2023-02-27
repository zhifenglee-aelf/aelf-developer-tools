using System.Collections.Immutable;
using System.Linq;

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace AElf.ContractDetector
{
    public class FindContractPath:Task
    {
        [Required]
        public ITaskItem[] Contract { get; set; }
        [Required]
        public ITaskItem[] ReferencePath { get; set; }
        [Output]
        public ITaskItem[] ContractPath { get; private set; }

        public override bool Execute()
        {
            var packageIds = Contract.Select(x => x.ToString()).ToImmutableHashSet();
            ContractPath = ReferencePath.Where(x=>packageIds.Contains(x.GetMetadata("NuGetPackageId"))).ToArray();
            return true;
        }
    }    
}