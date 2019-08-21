using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRS.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string role { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}