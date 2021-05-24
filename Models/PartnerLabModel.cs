using System;
using System.ComponentModel.DataAnnotations;

namespace LabReservation.Models
{
    public class PartnerLab
    {
        public string toolName { get; set; }
        public int maxCount { get; set; }
        public string room { get; set; }
        public int slot1 { get; set; }
        public int slot2 { get; set; }
        public int slot3 { get; set; }
        public int slot4 { get; set; }
        public int slot5 { get; set; }
        public int slot6 { get; set; }
    }
}