using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRS.Models
{
    public class BookRoomViewModel
    {
        public List<Hotels> hotels { get; set; }
        public List<Room> rooms { get; set; }
    }
}