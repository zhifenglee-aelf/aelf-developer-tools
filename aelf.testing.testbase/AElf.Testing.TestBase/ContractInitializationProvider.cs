using System.Collections.Generic;
using AElf.Kernel.SmartContract.Application;
using AElf.Sdk.CSharp;
using AElf.Types;

namespace AElf.Testing.TestBase
{
    public class ContractInitializationProvider<T> : IContractInitializationProvider where T :CSharpSmartContractAbstract
    {
        public List<ContractInitializationMethodCall> GetInitializeMethodList(byte[] contractCode)
        {
            return new List<ContractInitializationMethodCall>();
        }

        public Hash SystemSmartContractName { get; } = SmartContractAddressNameProvider.Name;
        public string ContractCodeName { get; } = (typeof(T).Assembly.FullName??"").Split(",")[0];
    }
}