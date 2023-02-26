using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Check;

var asm = Assembly.LoadFile(@"C:\Users\User\repo\aelf-boilerplate\chain\src\AElf.Boilerplate.TestBase\bin\Debug\net6.0\AElf.Boilerplate.TestBase.dll");
var name = asm.FullName.Split(",")[0];
var prefix = $"{name}.assets.contract.";
foreach (var manifestResourceName in asm.GetManifestResourceNames())
{
    var contractName = manifestResourceName.Replace(prefix, "").Replace(".dll", "");
    Console.WriteLine(contractName);
}

var bytes = new List<byte>();
var stream = asm.GetManifestResourceStream(asm.GetManifestResourceNames()[0]);
using (BinaryReader reader = new BinaryReader(stream))
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

Helpers.ByteArrayToFile(@"C:\Users\User\repo\aelf-boilerplate\chain\src\AElf.Boilerplate.TestBase\bin\Debug\net6.0\Another.dll", bytes.ToArray());
// Console.WriteLine(Convert.ToBase64String(bytes.ToArray()));

