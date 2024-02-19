using System;
using System.Collections.Generic;

namespace L.I.S.A.Models
{
    public partial class Trip
    {
        public long TripsId { get; set; }
        public long TruckId { get; set; }
        public long OffloadSiteId { get; set; }
        public long LoadSiteId { get; set; }
        public long FleetId { get; set; }
        public long OffloadSlipId { get; set; }
        public long LoadSlipId { get; set; }
        public long DriverId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual Driver Driver { get; set; } = null!;
        public virtual Fleet Fleet { get; set; } = null!;
        public virtual LoadingSite LoadSite { get; set; } = null!;
        public virtual LoadingSlip LoadSlip { get; set; } = null!;
        public virtual OffloadingSite OffloadSite { get; set; } = null!;
        public virtual OffloadingSlip OffloadSlip { get; set; } = null!;
    }
}
