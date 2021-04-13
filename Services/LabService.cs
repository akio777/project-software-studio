using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mime;
using LabReservation.Models;
using LabReservation.Data;
using Newtonsoft.Json;

namespace LabReservation.Services
{
    public interface ILabService
    {
        Return Read(int labid, int userid);

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

        public Return Read(int labid, int userid)
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
                        reserve_time = labres.reserveinfo.start_time,
                        reserve_by = labres.reserveinfo.reserve_by
                    }
                ).OrderBy(arg => arg.reserve_time);
            
            // IDictionary<string, object> main_data = new Dictionary<string, object>();
            for (var day = 0; day < 7; day++)
            {
                int this_day = dateNow.Day + day;
                var data_day = temp.Where(data => data.reserve_time.Day == this_day);
                var reserved = temp.Where(data => data.reserve_by == userid);
                var mine = (from data in data_day where data.reserve_by == userid select data.reserve_time.Hour).ToArray();
                // IDictionary<string, object> map_data = new Dictionary<string, object>();
                Reserve_page map_data = new Reserve_page {};
                map_data.day = day;
                Console.WriteLine(mine.GetType());
                map_data.reserved = mine;
                foreach (var time in time_slot)
                {
                    map_data.timeslot.Append(time);
                    // map_data[time.ToString()] = maxall-(data_day.Where(data => data.reserve_time.Hour == time).Count()); 
                }

                // main_data[day.ToString()] = map_data;
            }
            // Console.WriteLine(JsonConvert.SerializeObject(main_data, Formatting.Indented));
            return new Return
            {
                Error = false,
                Data = "",
            };
        }

        public Return Get()
        {
            List<bool> lab = new List<bool> {false, false, false, false, false};
            for (var i = 0; i < 5; i++)
            {
                var temp = Read(i + 1, 1);
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