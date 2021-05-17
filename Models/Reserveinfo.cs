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

    public class ReserveinfoProps
    {
        public Labinfo labinfo { get; set; }
        public Reserve_page[] reservePageList { get; set; }
        public ReservedInput reservedInput { get; set; }
        public ReserveinfoProps(Reserve_page[] _reservePageList, Labinfo _labinfo)
        {
            reservedInput = new ReservedInput();
            reservePageList = _reservePageList;
            labinfo = _labinfo;
        }
    }
    public class Reserve_page
    {
        public int day { get; set; }
        public int[] reserved { get; set; }
        public int[] notAvailable { get; set; }
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
        public int? lab_id { get; set; }

        public CancelMap()
        {
            reserve_id = -1;
            lab_name = "";
        }

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

        public ReservedInput()
        {
            time_0 = new bool[] { false, false, false, false, false, false, false };
            time_1 = new bool[] { false, false, false, false, false, false, false };
            time_2 = new bool[] { false, false, false, false, false, false, false };
            time_3 = new bool[] { false, false, false, false, false, false, false };
            time_4 = new bool[] { false, false, false, false, false, false, false };
            time_5 = new bool[] { false, false, false, false, false, false, false };
            time_6 = new bool[] { false, false, false, false, false, false, false };
            time_7 = new bool[] { false, false, false, false, false, false, false };
            time_8 = new bool[] { false, false, false, false, false, false, false };
            time_9 = new bool[] { false, false, false, false, false, false, false };
        }
    }
    public class CancelReserved
    {
        public int reserve_id { get; set; }
        public CancelReserved(int _reserve_id)
        {
            reserve_id = _reserve_id;
        }
    }

    public class LabManageInfoProps
    {
        public int id { get; set; }
        public Labinfo labinfo { get; set; }
        public Equipment equipment { get; set; }
        public Reserve_page[] reservePageList { get; set; }
        public LabShowForCancel cancelUserList { get; set; }
        public bool[] checkedList { get; set; }


        public bool modalOpen { get; set; }
        public ReservedInput reservedInput { get; set; }

        public LabManageInfoProps(Labinfo _labinfo, Equipment _equipment, Reserve_page[] _reservePageList, bool open = false)
        {
            reservedInput = new ReservedInput();
            modalOpen = open;
            equipment = _equipment;
            labinfo = _labinfo;
            reservePageList = _reservePageList;

        }
    }

    public class CancelMyReservedInput
    {
        public CancelMapOutput?[] time_0 { get; set; }
        public CancelMapOutput?[] time_1 { get; set; }
        public CancelMapOutput?[] time_2 { get; set; }
        public CancelMapOutput?[] time_3 { get; set; }
        public CancelMapOutput?[] time_4 { get; set; }
        public CancelMapOutput?[] time_5 { get; set; }
        public CancelMapOutput?[] time_6 { get; set; }
        public CancelMapOutput?[] time_7 { get; set; }
        public CancelMapOutput?[] time_8 { get; set; }
        public CancelMapOutput?[] time_9 { get; set; }

    }

    public class CancelMapOutput : CancelMap
    {
        public bool selected { get; set; }

        public CancelMapOutput(CancelMap? cancelMap)
        {
            reserve_id = cancelMap.reserve_id;
            day = cancelMap.day;
            time = cancelMap.time;
            lab_name = cancelMap.lab_name;
            lab_id = cancelMap.lab_id;
            selected = false;
        }
    }

    public class MyReserveProps
    {
        public CancelMyReservedInput cancelMyReservedInput { get; set; }
        public ReservedInput reservedInput { get; set; }
        public CancelMap[] cancelReservedModalInput { get; set; }
        public bool modalOpen { get; set; }
        public Labinfo[] filterList { get; set; }
        public MyReserveProps(CancelMyReservedInput _cancelMyReservedInput)
        {
            reservedInput = new ReservedInput();
            cancelMyReservedInput = _cancelMyReservedInput;
        }
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
        public DateTime day { get; set; }
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