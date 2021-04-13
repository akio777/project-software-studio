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
        Return Read(int labid);

        Return Get();
    }

    public class LabService : ILabService
    {
        private readonly LabReservationContext db;
        public int[] time_slot = Enumerable.Range(0+8, 11).ToArray();
        public LabService(LabReservationContext context)
        {
            db = context;
        }

        public Return Read(int labid)
        {
            var dateNow = DateTime.Now;
            var maxall = db.Equipment.Where(data => data.lab_id == labid).First().maximum;
            var temp = db.Labinfo
                .Join(
                    db.Reserveinfo,
                    labinfo => labinfo.id,
                    reserveinfo => reserveinfo.lab_id,
                    (labinfo, reserveinfo) => new {labinfo, reserveinfo}
                ).Where(x =>
                    (x.reserveinfo.start_time.Day - dateNow.Day) >= 0 &&
                    (x.reserveinfo.start_time.Day - dateNow.Day) <= 6 &&
                    x.labinfo.id == labid
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

            List<dynamic> days = new List<dynamic>();
            for (var day = 0; day < 7; day++)
            {
                var this_day = temp.Where(data => data.reserve_time.Day == day + data.reserve_time.Day);
                List<dynamic> tempday = new List<dynamic>();
                foreach (var time in time_slot)
                {
                    tempday.Add(maxall-this_day.Where(data => data.reserve_time.Hour == time).Count());
                }
                days.Add(tempday);
            }
            
            List<dynamic> data = new List<dynamic>{maxall, days};
            
            return new Return
            {
                Error = false,
                Data = data,
            };
        }

        public Return Get()
        {
            List<bool> lab = new List<bool> {false, false, false, false, false};
            for (var i = 0; i < 5; i++)
            {
                var temp = Read(i + 1);
                var data = temp.Data[1];
                foreach (var days in data)
                {
                    foreach (var day in days)
                    {
                        if (day <= 0)
                        {
                            lab[i] = true;
                            break;
                        }
                    }
                    if (lab[i]) break;
                }
            }

            var lab_info = db.Labinfo
                .Join(
                    db.Equipment,
                    labinfo => labinfo.id,
                    equipment => equipment.lab_id,
                    (labinfo, equipment) => new
                    {
                        status = lab[labinfo.id - 1],
                        name = labinfo.name,
                        equip = labinfo.equip,
                        lab_id = labinfo.id
                    }
                );
            return new Return
            {
                Error = false,
                Data = lab_info
            };
        }
    }
}