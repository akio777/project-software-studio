using System;
using System.ComponentModel.DataAnnotations;

namespace LabReservation.Models
{
    public class Toolinfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int update_by { get; set; }
        public int created_by { get; set; }

        [DataType(DataType.Date)]
        public DateTime created_date { get; set; }
        [DataType(DataType.Date)]
        public DateTime update_date { get; set; }
    }
}