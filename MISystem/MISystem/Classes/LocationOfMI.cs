using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISystem.Classes
{
    public class LocationOfMI : Element
    {
        public LocationOfMI()
        {
            MeasurInstrs = new Dictionary<int, MeasurInstr>();
        }
        public int NumberLocationOfMI { get; set; }
        public string NameLocationOfMI { get; set; }
        public int ProductionFacilityId { get; set; }
        public ProductionFacility ProductionFacility { get; set; }
        public Dictionary<int, MeasurInstr> MeasurInstrs { get; set; }
    }
}
