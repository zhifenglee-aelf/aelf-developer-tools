#region Copyright notice and license

#endregion

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace AElf.Tools
{
    /// <summary>
    /// A helper task to resolve actual OS type and bitness.
    /// </summary>
    public class ProtoToolsPlatform : Task
    {
        /// <summary>
        /// Return one of 'linux', 'macosx' or 'windows'.
        /// If the OS is unknown, the property is not set.
        /// </summary>
        [Output]
        public string Os { get; set; }

        /// <summary>
        /// Return one of 'x64', 'x86', 'arm64'.
        /// If the CPU is unknown, the property is not set.
        /// </summary>
        [Output]
        public string Cpu { get; set; }


        public override bool Execute()
        {
            switch (Platform.Os)
            {
                case CommonPlatformDetection.OSKind.Linux: Os = "linux"; break;
                case CommonPlatformDetection.OSKind.MacOSX: Os = "macos"; break;
                case CommonPlatformDetection.OSKind.Windows: Os = "windows"; break;
                default: Os = ""; break;
            }

            switch (Platform.Cpu)
            {
                case CommonPlatformDetection.CpuArchitecture.X86: Cpu = "x86"; break;
                case CommonPlatformDetection.CpuArchitecture.X64: Cpu = "x64"; break;
                case CommonPlatformDetection.CpuArchitecture.Arm64: Cpu = "arm64"; break;
                default: Cpu = ""; break;
            }

            // Use x64 on macosx arm64 until a AElf.Tools protoc is shipped
            if (Os == "macos" && Cpu == "arm64")
            {
                Cpu = "x64";
            }
            // Use x86 on Windows arm64 until a AElf.Tools protoc is shipped
            else if (Os == "windows" && Cpu == "arm64")
            {
                Cpu = "x86";
            }

            return true;
        }
    };
}