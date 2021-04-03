using System;
using System.ComponentModel.DataAnnotations;

namespace LabReservation.Models
{
    public class Equipment
    {
        public int id { get; set; }
        public int lab_id { get; set; }
        public int maximum { get; set; }

    }
}