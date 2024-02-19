using System;
using System.Collections.Generic;

namespace L.I.S.A.Models
{
    public partial class Admin
    {
        public long AdminId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public long ContactNum { get; set; }
        public string Password { get; set; } = null!;
    }
}
