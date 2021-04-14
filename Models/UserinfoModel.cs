using System;
using System.ComponentModel.DataAnnotations;
using LabReservation.Services;
using Microsoft.AspNetCore.Identity;

namespace LabReservation.Models
{
    public class Userinfo
    {
        
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int role { get; set; }

    }
}