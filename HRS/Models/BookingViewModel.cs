using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRS.Models
{
    public class BookingViewModel
    {
        public List<Book> books { get; set; }
        public List<Hotels> hotels { get; set; }
    }
}