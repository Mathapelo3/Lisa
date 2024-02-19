using System;
using System.Collections.Generic;

namespace L.I.S.A.Models
{
    public partial class OffloadingSite
    {
        public OffloadingSite()
        {
            Trips = new HashSet<Trip>();
        }

        public long OffloadSiteId { get; set; }
        public string OffloadSiteName { get; set; } = null!;

        public virtual ICollection<Trip> Trips { get; set; }
    }
}
