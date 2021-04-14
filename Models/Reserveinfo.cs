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

    public class Reserved
    {
        public int day { get; set; }
        public int time { get; set; }
        public int lab_id { get; set; }
    }

    public class CancelMap
    {
        public int reserve_id { get; set; }
        public int day { get; set; }
        public int time { get; set; }
    }

    public class ReservedInput
    {
        public bool[] time_0 { get; set; }
        public bool[] time_1 { get; set; }
        public bool[] time_2 { get; set; }
        public bool[] time_3 { get; set; }
        public bool[] time_4 { get; set; }
        public bool[] time_5 { get; set; }
        public bool[] time_6 { get; set; }
        public bool[] time_7 { get; set; }
        public bool[] time_8 { get; set; }
        public bool[] time_9 { get; set; }
    }
}