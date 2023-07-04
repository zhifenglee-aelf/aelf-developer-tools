#region Copyright notice and license

#endregion

using System.Collections.Generic;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace AElf.Tools
{
    public class ProtoReadDependencies : Task
    {
        /// <summary>
        /// The collection is used to collect possible additional dependencies
        /// of proto files cached under ProtoDepDir.
        /// </summary>
        [Required]
        public ITaskItem[] Protobuf { get; set; }

        /// <summary>
        /// Directory where protoc dependency files are cached.
        /// </summary>
        [Required]
        public string ProtoDepDir { get; set; }

        /// <summary>
        /// Additional items that a proto file depends on. This list may include
        /// extra dependencies; we do our best to include as few extra positives
        /// as reasonable to avoid missing any. The collection item is the
        /// dependency, and its Source metadatum is the dependent proto file, like
        ///     <ItemName Include="/usr/include/proto/wrapper.proto"
        ///               Source="my_proto.proto" />
        /// </summary>
        [Output]
        public ITaskItem[] Dependencies { get; private set; }

        public override bool Execute()
        {
            // Read dependency files, where available. There might be none,
            // just use a best effort.
            if (ProtoDepDir != null)
            {
                var dependencies = new List<ITaskItem>();
                foreach (var proto in Protobuf)
                {
                    string[] deps = DepFileUtil.ReadDependencyInputs(ProtoDepDir, proto.ItemSpec, Log);
                    foreach (string dep in deps)
                    {
                        var ti = new TaskItem(dep);
                        ti.SetMetadata(Metadata.Source, proto.ItemSpec);
                        dependencies.Add(ti);
                    }
                }
                Dependencies = dependencies.ToArray();
            }
            else
            {
                Dependencies = new ITaskItem[0];
            }

            return !Log.HasLoggedErrors;
        }
    };
}
