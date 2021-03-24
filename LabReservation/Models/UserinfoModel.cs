using System;
using System.ComponentModel.DataAnnotations;

namespace LabReservation.Models
{
    public class Userinfo
    {
        public int id { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public int update_by { get; set; }
        public int created_by { get; set; }

        [DataType(DataType.Date)]
        public DateTime created_date { get; set; }
        [DataType(DataType.Date)]
        public DateTime update_date { get; set; }
    }
}