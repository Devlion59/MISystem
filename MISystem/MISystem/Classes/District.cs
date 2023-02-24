using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISystem.Classes
{
    public class District:Element
    {
        public District()
        {
            OandGPFs = new Dictionary<int, OandGPF>();
        }
        public string NameDistrict { get; set; }
        public Dictionary<int, OandGPF> OandGPFs { get; set; }
    }
}
