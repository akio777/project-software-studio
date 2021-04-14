using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LabReservation.Models
{
    public class Labinfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string equip { get; set; }

    }
}