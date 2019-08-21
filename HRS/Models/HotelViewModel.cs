using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HRS.Models
{
    public class HotelViewModel
    {
        public List<Hotels> hotels { get; set; }
        public List<Room> rooms { get; set; }
        public List<Types> types { get; set; }
        public List<Users> users { get; set; }
    }
}