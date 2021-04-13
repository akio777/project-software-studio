using System;
using System.ComponentModel.DataAnnotations;

namespace LabReservation.Models
{
    public class Reserveinfo
    {
        public int id { get; set; }
        public int lab_id { get; set; }
        public int reserve_by { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime start_time { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime end_time { get; set; }
    }


    public class Reserve_page
    {
        public int day { get; set; }
        public Int32[] reserved { get; set; }
        public Int32[] timeslot { get; set; }
    }
}