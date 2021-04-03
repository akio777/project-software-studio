using System;
using System.ComponentModel.DataAnnotations;

namespace LabReservation.Models
{
    public class Blacklist
    {
        public int id { get; set; }
        public int user_id { get; set; }

    }
}