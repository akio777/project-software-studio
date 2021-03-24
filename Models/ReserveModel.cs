using System;
using System.ComponentModel.DataAnnotations;

namespace LabReservation.Models
{
    public class Reserve
    {
        public int id { get; set; }
        public int store_id { get; set; }
        public int created_by { get; set; }

        [DataType(DataType.Date)]
        public DateTime start_date { get; set; }
        [DataType(DataType.Date)]
        public DateTime end_date { get; set; }
    }
}