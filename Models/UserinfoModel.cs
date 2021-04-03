using System;
using System.ComponentModel.DataAnnotations;

namespace LabReservation.Models
{
    public class Userinfo
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }

    }
}