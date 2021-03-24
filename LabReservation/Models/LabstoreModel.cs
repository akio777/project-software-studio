using System;
using System.ComponentModel.DataAnnotations;

namespace LabReservation.Models
{
    public class Labstore
    {
        public int id { get; set; }
        public int lad_id { get; set; }
        public int tool_id { get; set; }
        public int update_by { get; set; }
        public int created_by { get; set; }

        [DataType(DataType.Date)]
        public DateTime created_date { get; set; }
        [DataType(DataType.Date)]
        public DateTime update_date { get; set; }
    }
}