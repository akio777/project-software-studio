using System;
using System.ComponentModel.DataAnnotations;

namespace LabReservation.Models
{
    public class Labinfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string equip { get; set; }

    }

    public class LabCardInfo
    {
        public bool notAvailable { get; set; }
        public string name { get; set; }
        public string equip { get; set; }
        public int lab_id { get; set; }
    }

    public class LabManageInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string equip { get; set; }
        public int amount { get; set; }
    }
}