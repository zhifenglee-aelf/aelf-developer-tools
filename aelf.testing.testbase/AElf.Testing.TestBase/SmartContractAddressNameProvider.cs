using AElf.Kernel.Infrastructure;
using AElf.Types;

namespace AElf.Testing.TestBase;

public class SmartContractAddressNameProvider
{
    public static readonly Hash Name = HashHelper.ComputeFrom("AElf.ContractNames.Test");
    public static readonly string StringName = Name.ToStorageKey();
    public Hash ContractName => Name;
    public string ContractStringName => StringName;
}