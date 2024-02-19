using System;
using System.Collections.Generic;

namespace L.I.S.A.Models
{
    public partial class Permit
    {
        public long PermitId { get; set; }
        public long DriverId { get; set; }
        public string PermitType { get; set; } = null!;
        public DateTime IssueDate { get; set; }
        public DateTime ExpDate { get; set; }
        public string PermitStatus { get; set; } = null!;
        public byte[] PermitImg { get; set; } = null!;

        public virtual Driver Driver { get; set; } = null!;
    }
}
