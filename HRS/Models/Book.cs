using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRS.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}