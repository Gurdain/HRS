using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRS.Models
{
    public class UserViewModel
    {
        public List<Users> users { get; set; }
        public List<Role> roles { get; set; }
    }
}