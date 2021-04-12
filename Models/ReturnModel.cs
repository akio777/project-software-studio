using System;
using System.ComponentModel.DataAnnotations;

namespace LabReservation.Models
{
    public class Return
    {
        public Boolean Error {get; set;}
        public dynamic Data {get; set;}
    }
}