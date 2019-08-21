using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Utility
{
    public class Exceptions
    {
        public int ExceptionId { get; set; }
        public string ExceptionNumber { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionUrl { get; set; }
        public string ExceptionMethod { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public DateTime deletedOn { get; set; }
        public bool isDeleted { get; set; }
    }
}