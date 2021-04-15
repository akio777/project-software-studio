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
        public string lab_name { get; set; }
        public int lab_id { get; set; }
    }

    public class CancelReserved
    {
        public int reserve_id { get; set; }
    }

    public class WhoReserved
    {
        public int reserved_id { get; set; }
        public int user_id { get; set; }
        public string email { get; set; }
    }
    public class LabShowForCancel
    {
        public string name { get; set; }
        public int lab_id { get; set; }
        public WhoReserved[] data { get; set; }
        public int start_time { get; set; }
        public int end_time { get; set; }
    }

    public class UserEmail
    {
        public int user_id { get; set; }
        public string email { get; set; }
    }
    
    public class BlackListPage
    {
        public UserEmail[] NotBlock { get; set; }
        public UserEmail[] wasBlock { get; set; }
    }
    
}