using System;
using System.Collections.Generic;

namespace L.I.S.A.Models
{
    public partial class Fleet
    {
        public Fleet()
        {
            Trips = new HashSet<Trip>();
        }

        public long FleetId { get; set; }
        public long TruckId { get; set; }
        public string Activation { get; set; } = null!;

        public virtual Truck Truck { get; set; } = null!;
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
