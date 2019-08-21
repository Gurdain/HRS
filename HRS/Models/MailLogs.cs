using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRS.Models
{
    public class MailLogs
    {
        public int MailId { get; set; }
        public string ToAddress { get; set; }
        public string FromAdress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool EmailStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}