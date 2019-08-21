using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRS.Models
{
    public class Hotels
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Locality { get; set; }
        public string Description { get; set; }
        public int HotelTypeId { get; set; }
        public int UserId { get; set; }
        public int Rooms { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}