using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AElf.ContractTestBase;
using Google.Protobuf.WellKnownTypes;

namespace AElf.Boilerplate.TestBase;

public class ContractCodeProvider : IContractCodeProvider
{
    private IReadOnlyDictionary<string, byte[]> _codes;

    public ContractCodeProvider()
    {
    }

    public IReadOnlyDictionary<string, byte[]> Codes
    {
        get
        {
            if (_codes == null)
            {
                PrepareCodes();
            }

            return _codes;
        }
        set
        {
            PrepareCodes();

            var dict = new Dictionary<string, byte[]>();
            foreach (var (key, bytes) in _codes)
            {
                dict[key] = bytes;
            }

            foreach (var (key, bytes) in value)
            {
                dict[key] = bytes;
            }

            _codes = dict;
        }
    }

    private static byte[] GetResourceBytes(Assembly asm, string resourceName)
    {
        var bytes = new List<byte>();
        var stream = asm.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            return new byte[] { };
        }

        using (var reader = new BinaryReader(stream))
        {
            while (true)
            {
                var thisBytes = reader.ReadBytes(1024);
                if (thisBytes.Length == 0)
                {
                    break;
                }

                bytes.AddRange(thisBytes);
            }
        }

        return bytes.ToArray();
    }

    private void PrepareCodes()
    {
        var asm = Assembly.GetExecutingAssembly();
        var asmName = (asm.FullName ?? "").Split(",")[0];
        var prefix = $"{asmName}.assets.contract.";
        var codes = asm.GetManifestResourceNames().Where(name => name.StartsWith(prefix))
            .Select(name =>
            {
                var contractName = name.Replace(prefix, "").Replace(".dll", "");
                var contractCode = GetResourceBytes(asm, name);
                return (contractName, contractCode);
            })
            .ToDictionary(x => x.contractName, x => x.contractCode);
        _codes = codes;
    }
}