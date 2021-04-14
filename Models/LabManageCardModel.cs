using System;
using System.ComponentModel.DataAnnotations;

namespace LabReservation.Models
{
    public class LabManageCard
    {
        public Labinfo labinfo { get; set; }

        public Equipment equipinfo { get; set; }
    }
}