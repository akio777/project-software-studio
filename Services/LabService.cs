using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mime;
using LabReservation.Models;
using LabReservation.Data;

namespace LabReservation.Services
{
    public interface ILabService
    {
        Return Read();
    }

    public class LabService : ILabService
    {
        private readonly LabReservationContext db;
        public int[] time_slot = Enumerable.Range(0+8, 11).ToArray();
        public LabService(LabReservationContext context)
        {
            db = context;
        }

        public Return Read()
        {
            
            
            var dateNow = DateTime.Now;
            var maxall = db.Equipment.Select(e => e.maximum).ToList();
            var temp = db.Labinfo
                .Join(
                    db.Reserveinfo,
                    labinfo => labinfo.id,
                    reserveinfo => reserveinfo.lab_id,
                    (labinfo, reserveinfo) => new {labinfo, reserveinfo}
                ).Where(x =>
                    (dateNow.Day - x.reserveinfo.start_time.Day) >= 0 &&
                    (dateNow.Day - x.reserveinfo.start_time.Day) <= 7
                )
                .Join(
                    db.Equipment,
                    labres => labres.labinfo.id,
                    equipment => equipment.lab_id,
                    (labres, equipment) => new
                    {
                        lab_id = labres.labinfo.id,
                        equipment = labres.labinfo.equip,
                        reserve_time = labres.reserveinfo.start_time
                    }
                );

            List<dynamic>[,] count = new List<dynamic>[5,18];
            List<dynamic> lab = new List<dynamic>
            {
                temp.Where(data => data.lab_id == 1),
                temp.Where(data => data.lab_id == 2),
                temp.Where(data => data.lab_id == 3),
                temp.Where(data => data.lab_id == 4),
                temp.Where(data => data.lab_id == 5),
            };
            
           
            
            // foreach (var i in count)
            // {
            //     Console.WriteLine("1D : "+i.ToString());
            //     foreach (var j in i)
            //     {
            //         Console.WriteLine("2D : "+j.ToString());
            //     }
            // }
            // foreach (var time in time_slot)
            // {
            //     foreach (var data in temp)
            //     {
            //         if (data.reserve_time.Hour == time)
            //         {
            //             
            //         }
            //     }
            // }
            // var nowTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 9, 0, 0);
            

            
            return new Return
            {
                Error = false,
                Data = "xxxxx"
            };
        }
    }
}