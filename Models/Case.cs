using System;
using System.Collections.Generic;

namespace L.I.S.A.Models
{
    public partial class Case
    {
        public long CasingId { get; set; }
        public string CasingType { get; set; } = null!;
        public string CasingDesc { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = null!;
        public string Resolution { get; set; } = null!;
    }
}
