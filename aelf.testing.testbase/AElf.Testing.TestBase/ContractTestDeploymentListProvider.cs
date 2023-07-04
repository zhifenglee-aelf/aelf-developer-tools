using System.Collections.Generic;
using AElf.ContractTestBase;
using AElf.Kernel.SmartContract.Application;
using AElf.Types;

namespace AElf.Testing.TestBase
{
    public class SideChainDAppContractTestDeploymentListProvider : SideChainContractDeploymentListProvider, IContractDeploymentListProvider
    {
        public List<Hash> GetDeployContractNameList()
        {
            var list = base.GetDeployContractNameList();
            list.Add(SmartContractAddressNameProvider.Name);
            return list;
        }
    }
    
    public class MainChainDAppContractTestDeploymentListProvider : MainChainContractDeploymentListProvider, IContractDeploymentListProvider
    {
        public List<Hash> GetDeployContractNameList()
        {
            var list = base.GetDeployContractNameList();
            list.Add(SmartContractAddressNameProvider.Name);
            return list;
        }
    }
}