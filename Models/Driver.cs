using System;
using System.Collections.Generic;

namespace L.I.S.A.Models
{
    public partial class Driver
    {
        public Driver()
        {
            Permits = new HashSet<Permit>();
            Trips = new HashSet<Trip>();
        }

        public long DriverId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public long ContactNum { get; set; }
        public string BirthCountry { get; set; } = null!;
        public DateTime EmpDate { get; set; }
        public long EmpNum { get; set; }
        public long License { get; set; }
        public byte[] LicenseImg { get; set; } = null!;
        public string? UserId { get; set; }

        public virtual AspNetUser? User { get; set; }
        public virtual ICollection<Permit> Permits { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
