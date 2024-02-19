using System;
using System.Collections.Generic;

namespace L.I.S.A.Models
{
    public partial class OffloadingSlip
    {
        public OffloadingSlip()
        {
            Trips = new HashSet<Trip>();
        }

        public long OffloadSlipId { get; set; }
        public byte[] OffloadSlipImg { get; set; } = null!;

        public virtual ICollection<Trip> Trips { get; set; }
    }
}
