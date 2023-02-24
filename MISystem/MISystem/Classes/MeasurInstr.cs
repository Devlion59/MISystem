using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISystem.Classes
{
    public class MeasurInstr : Element
    {
        public int PhaseId { get; set; }
        public int MiFromRefId { get; set; }
        public int LocationOfMiId { get; set; }
        public string YearOfManufacture { get; set; }
        public string AccuracyClass { get; set; }
        public string SerialNumber { get; set; }
        public string CoefficientUp { get; set; }
        public string CoefficientDown { get; set; }
        public DateTime VerificationDate { get; set; }
        public int VerificationInterval { get; set; }
        public DateTime VerificationNextDate { get; set; }
        public LocationOfMI LocationOfMI { get; set; }
    }
}
