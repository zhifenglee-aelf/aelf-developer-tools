#region Copyright notice and license

#endregion

using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace AElf.Tools.Test
{
    static class Utils
    {
        // Build an item with a name from args[0] and metadata key-value pairs
        // from the rest of args, interleaved.
        // This does not do any checking, and expects an odd number of args.
        public static ITaskItem MakeItem(params string[] args)
        {
            var item = new TaskItem(args[0]);
            for (int i = 1; i < args.Length; i += 2)
            {
                item.SetMetadata(args[i], args[i + 1]);
            }
            return item;
        }

        // Return an array of items from given itemspecs.
        public static ITaskItem[] MakeSimpleItems(params string[] specs)
        {
            return specs.Select(s => new TaskItem(s)).ToArray();
        }
    };
}
