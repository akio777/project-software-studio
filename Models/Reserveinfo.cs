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
        public int[] reserved { get; set; }
        public int[] timeslot { get; set; }
        public int maximum { get; set; }
    }

    public class DayTime
    {
        public int day { get; set; }
        public int time { get; set; }
    }
    
    public class Reserve_confirm
    {
        public DayTime[] confirm { get; set; }
    }
    
}