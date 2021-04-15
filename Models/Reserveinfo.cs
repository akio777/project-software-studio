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

    public class CancelReserved
    {
        public int reserve_id { get; set; }
    }
    
    public class LabManageInfoProps
    {
        public Labinfo labinfo { get; set; } 
        public Equipment equipment { get; set; }
        public Reserve_page[] reservePageList { get; set; }

        public LabManageInfoProps(Labinfo _labinfo, Equipment _equipment, Reserve_page[] _reservePageList)
        {
            equipment = _equipment;
            labinfo = _labinfo;
            reservePageList = _reservePageList;
        }
    }
}