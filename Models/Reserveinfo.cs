using System;
using System.ComponentModel.DataAnnotations;

namespace LabReservation.Models
{
    public class Reserveinfo
    {
        public int id { get; set; }
        public int lab_id { get; set; }
        public int reserve_by { get; set; }
        [DataType(DataType.Date)]
        public DateTime start_time { get; set; }
        [DataType(DataType.Date)]
        public DateTime end_time { get; set; }
    }
}