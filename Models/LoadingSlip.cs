using System;
using System.Collections.Generic;

namespace L.I.S.A.Models
{
    public partial class LoadingSlip
    {
        public LoadingSlip()
        {
            Trips = new HashSet<Trip>();
        }

        public long LoadSlipId { get; set; }
        public byte[] LoadSlipImg { get; set; } = null!;

        public virtual ICollection<Trip> Trips { get; set; }
    }
}
