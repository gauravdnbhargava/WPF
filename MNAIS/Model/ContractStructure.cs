using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <Summary>Structure for Contract Import/Export</Summary>
/// <Author>Mani Sharma</Author>
/// <Date Created>17-10-2013</Date Created>
/// <Date Modified></Date Modified>
/// <Modified By></Modified By>

namespace MNAIS
{
    [Serializable]
    public class ContractStructure
    {
        public CreateContractViewModel contractVM { get; set; }
        public ModellingOptionsViewModel modellingVM { get; set; }
        public PricingOptionsViewModel pricingVM { get; set; }
        public ImportDataViewModel dataVM { get; set; }

        public ContractStructure() { }
    }
}
