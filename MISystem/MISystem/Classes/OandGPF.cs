using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISystem.Classes
{
    public class OandGPF : Element
    {
        public OandGPF()
        {
            ProductionFacilities = new Dictionary<int, ProductionFacility>();
        }
        public string NameOandGPF { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public Dictionary<int, ProductionFacility> ProductionFacilities { get; set; }
    }
}
