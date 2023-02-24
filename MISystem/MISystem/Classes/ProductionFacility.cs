using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISystem.Classes
{
    public class ProductionFacility : Element
    {
        public ProductionFacility()
        {
            LocationOfMIs = new Dictionary<int, LocationOfMI>();
        }
        public string NameProductFacility { get; set; }
        public int OandGPFId { get; set; }
        public OandGPF OandGPF { get; set; }
        public Dictionary<int, LocationOfMI> LocationOfMIs { get; set; }
    }
}
