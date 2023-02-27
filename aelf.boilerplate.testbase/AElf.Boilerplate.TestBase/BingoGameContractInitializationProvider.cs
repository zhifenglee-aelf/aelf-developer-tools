using System.Collections.Generic;
using AElf.Boilerplate.TestBase;
using AElf.Kernel.SmartContract.Application;
using AElf.Sdk.CSharp;
using AElf.Types;

namespace AElf.Boilerplate.TestBase
{
    public class BingoGameContractInitializationProvider<T> : IContractInitializationProvider where T :CSharpSmartContractAbstract
    {
        public List<ContractInitializationMethodCall> GetInitializeMethodList(byte[] contractCode)
        {
            return new List<ContractInitializationMethodCall>();
        }

        public Hash SystemSmartContractName { get; } = DAppSmartContractAddressNameProvider.Name;
        public string ContractCodeName { get; } = (typeof(T).Assembly.FullName??"").Split(",")[0];
    }
}