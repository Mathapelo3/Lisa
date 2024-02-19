using System;
using System.Collections.Generic;

namespace L.I.S.A.Models
{
    public partial class LoadingSite
    {
        public LoadingSite()
        {
            Trips = new HashSet<Trip>();
        }

        public long LoadSiteId { get; set; }
        public string LoadSiteName { get; set; } = null!;

        public virtual ICollection<Trip> Trips { get; set; }
    }
}
