using System.Collections.Generic;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace AElf.Tools
{
    public class ProtoTypeResolve: Task
    {
        [Required]
        public string NugetRoot { get; set; }
        [Required]
        public ITaskItem[] ProtoAllRef { get; set; }
        [Required]
        public ITaskItem[] ReferencePath { get; set; }
        [Output]
        public string[] TargetBaseProto { get; set; }
        [Output]
        public string[] TargetBasePath { get; set; }
        [Output]
        public string[] TargetRefProto { get; set; }
        [Output]
        public string[] TargetRefPath { get; set; }

        private const string ProtoTypeName = "ProtoType";
        private const string ProtoTypeBase = "base";
        private const string ProtoTypeRef = "reference";
        private const string ProtoIdentity = "Identity";
        private const string ProtoVersion = "Version";

        public override bool Execute()
        {
            List<string> protoBaseFiles = new List<string>();
            List<string> protoRefFiles = new List<string>();
            List<string> protoBasePaths = new List<string>();
            List<string> protoRefPaths = new List<string>();
            
            foreach (var packageReference in ProtoAllRef)
            {
                var protoType = string.IsNullOrEmpty(packageReference.GetMetadata(ProtoTypeName)) ? ProtoTypeBase : packageReference.GetMetadata(ProtoTypeName);
                var protoIdentity = packageReference.GetMetadata(ProtoIdentity);
                var protoVersion = packageReference.GetMetadata(ProtoVersion);
                var protoName = protoIdentity.Split('.')[1].ToLower();

                var protoNugetFile = NugetRoot + protoIdentity.ToLower() + "/" + protoVersion + "/" + "Protobuf/acs/" + protoName + ".proto";
                var protoNugetPath = NugetRoot + protoIdentity.ToLower() + "/" + protoVersion + "/" + "Protobuf";

                if (protoType.Equals(ProtoTypeBase))
                {
                    protoBaseFiles.Add(protoNugetFile);
                    protoBasePaths.Add(protoNugetPath);
                }
                else if (protoType.Equals(ProtoTypeRef))
                {
                    protoRefFiles.Add(protoNugetFile);
                    protoRefPaths.Add(protoNugetPath);
                }
                else
                {
                    Log.LogWarning("ProtoType should be base or reference, otherwise will put it to base folder");
                    protoBaseFiles.Add(protoNugetFile);
                    protoBasePaths.Add(protoNugetPath);
                }
            }

            TargetBaseProto = protoBaseFiles.ToArray();
            TargetBasePath = protoBasePaths.ToArray();
            TargetRefProto = protoRefFiles.ToArray();
            TargetRefPath = protoRefPaths.ToArray();

            return true;
        }
    }    
}