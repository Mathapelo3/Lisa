using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace L.I.S.A.Areas.Identity.Data;

// Add profile data for application users by adding properties to the LISAUser class
public class LISAUser : IdentityUser
{
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string contact_num { get; set; }
}

