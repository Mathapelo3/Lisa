using System;
using System.Collections.Generic;

namespace L.I.S.A.Models
{
    public partial class Truck
    {
        public Truck()
        {
            Fleets = new HashSet<Fleet>();
        }

        public long TruckId { get; set; }
        public string Make { get; set; } = null!;
        public string VinNum { get; set; } = null!;
        public string Trailer1Reg { get; set; } = null!;
        public string Trailer2Reg { get; set; } = null!;
        public string Company { get; set; } = null!;
        public string TruckCondition { get; set; } = null!;
        public string TruckStatus { get; set; } = null!;
        public byte[] TruckImg { get; set; } = null!;

        public virtual ICollection<Fleet> Fleets { get; set; }
    }
}
