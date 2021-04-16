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
        Return LabInfo(int userid);
        Return LabManageInfo();
        Return Confirm(Reserved[] data, int userid);
        Return ReadCancel(int userid);
        Return Cancel(CancelReserved[] data);
        Return LabManage(int labid);
        Return CancelList(Reserved data);
        Return BlackListInfo();
        Return UnBlock(int userid);
        Return ForceBlock(int userid);
        Return GetEmailUser(int userid);
    }
    // Console.WriteLine(JsonConvert.SerializeObject(all, Formatting.Indented));
    public class LabService : ILabService
    {
        private readonly LabReservationContext db;
        public int[] time_slot = Enumerable.Range(0 + 8, 10).ToArray();
        public int[] day_slot = Enumerable.Range(0, 7).ToArray();
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
                    (labinfo, reserveinfo) => new { labinfo, reserveinfo }
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
            List<Reserve_page> all = new List<Reserve_page>();
            for (var day = 0; day < 7; day++)
            {
                int this_day = dateNow.Day + day;
                var data_day = temp.Where(data => data.reserve_time.Day == this_day);
                var reserved = temp.Where(data => data.reserve_by == userid);
                var mine = (from data in data_day where data.reserve_by == userid select time_slot.ToList().IndexOf(data.reserve_time.Hour)).ToArray();
                // IDictionary<string, object> map_data = new Dictionary<string, object>();
                List<int> tempINT = new List<int>();
                foreach (var time in time_slot)
                {
                    // map_data.timeslot.Append(time);
                    tempINT.Add(maxall - (data_day.Where(data => data.reserve_time.Hour == time).Count()));
                }
                Reserve_page map_data = new Reserve_page { day = day, reserved = mine, timeslot = tempINT.ToArray(), maximum = maxall };

                all.Add(map_data);
            }

            // Console.WriteLine(JsonConvert.SerializeObject(all, Formatting.Indented));
            return new Return
            {
                Error = false,
                Data = all.ToArray(),
            };
        }

        public Return LabInfo(int userid)
        {
            List<bool> lab = new List<bool> { false, false, false, false, false };
            for (var i = 0; i < 5; i++)
            {
                var temp = Read(i + 1, userid);
                foreach (Reserve_page data in temp.Data)
                {
                    var check = data.timeslot.Contains(0);
                    if (check)
                    {
                        lab[i] = true;
                        break;
                    }
                }

                if (lab[i]) break;
            }

            var lab_info = db.Labinfo
                .Join(
                    db.Equipment,
                    labinfo => labinfo.id,
                    equipment => equipment.lab_id,
                    (labinfo, equipment) => new LabCardInfo
                    {
                        notAvailable = lab[labinfo.id - 1],
                        name = labinfo.name,
                        equip = labinfo.equip,
                        lab_id = labinfo.id
                    }
                );
            return new Return
            {
                Error = false,
                Data = lab_info.ToList()
            };
        }

        public Return Confirm(Reserved[] data, int userid)
        {
            var dateNow = DateTime.Now;
            // var dateMock = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day + 1, 23,0,0);
            foreach (var i in data)
            {
                var start_date = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day + i.day, time_slot[i.time], 0, 0);
                var end_date = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day + i.day, time_slot[i.time] + 1, 0, 0);
                var reserved = db.Reserveinfo.Add(new Reserveinfo
                {
                    lab_id = i.lab_id,
                    reserve_by = userid,
                    start_time = start_date,
                    end_time = end_date
                });
                db.SaveChanges();
            }
            return new Return
            {
                Error = false,
                Data = ""
            };
        }

        public Return LabManageInfo()
        {
            var temp = db.Labinfo
                .Join(
                    db.Equipment,
                    labinfo => labinfo.id,
                    equipment => equipment.lab_id,
                    (labinfo, equipment) => new LabManageInfo
                    { id = labinfo.id, name = labinfo.name, equip = labinfo.equip, amount = equipment.maximum }
                ).ToArray();
            return new Return
            {
                Error = false,
                Data = temp.ToArray()
            };
        }

        public Return ReadCancel(int userid)
        {
            var dateNow = DateTime.Now;
            var temp = db.Labinfo
                .Join(
                    db.Reserveinfo,
                    labinfo => labinfo.id,
                    reserveinfo => reserveinfo.lab_id,
                    (labinfo, reserveinfo) => new { labinfo, reserveinfo }
                ).Where(x =>
                    (x.reserveinfo.start_time.Day - dateNow.Day) >= 0 &&
                    (x.reserveinfo.start_time.Day - dateNow.Day) <= 6 &&
                    x.reserveinfo.reserve_by == userid
                )
                .Join(
                    db.Equipment,
                    labres => labres.labinfo.id,
                    equipment => equipment.lab_id,
                    (labres, equipment) => new
                    {
                        reserve_id = labres.reserveinfo.id,
                        name = labres.labinfo.name,
                        start_time = labres.reserveinfo.start_time,
                        end_time = labres.reserveinfo.end_time,
                        lab_name = labres.labinfo.name,
                        lab_id = labres.labinfo.id
                    }
                ).OrderBy(arg => arg.reserve_id);
            List<CancelMap> output = new List<CancelMap>();
            foreach (var data in temp)
            {
                // Console.WriteLine(day_slot.ToList().IndexOf(data.start_time.Day-dateNow.Day)+" :   "+time_slot.ToList().IndexOf(data.start_time.Hour));
                output.Add(new CancelMap
                {
                    reserve_id = data.reserve_id,
                    day = day_slot.ToList().IndexOf(data.start_time.Day - dateNow.Day),
                    time = time_slot.ToList().IndexOf(data.start_time.Hour),
                    lab_id = data.lab_id,
                    lab_name = data.lab_name
                });
            }
            return new Return
            {
                Error = false,
                Data = output
            };
        }

        public Return Cancel(CancelReserved[] data)
        {
            bool error = false;
            foreach (var i in data)
            {
                var check = db.Reserveinfo.Where(x => x.id == i.reserve_id).FirstOrDefault();
                if (check == null)
                {
                    error = true;
                    break;
                }
                var temp = (from a in db.Reserveinfo where a.id == i.reserve_id select a).Single();
                db.Reserveinfo.Remove(temp);
                db.SaveChanges();

            }
            if (error)
            {
                return new Return
                {
                    Error = true,
                    Data = "ไม่สามารถยกเลิกได้ ข้อมูลไม่ถูกต้อง กรุณา refresh"
                };
            }
            return new Return
            {
                Error = false,
                Data = ""
            };
        }


        public Return LabManage(int lab_id)
        {
            var dateNow = DateTime.Now;
            var data = db.Labinfo
                .Where(labinfo => labinfo.id == lab_id)
                .Join(db.Equipment, labinfo => labinfo.id, equipment => equipment.lab_id,
                    (labinfo, equipment) => new
                    {
                        lab_id = labinfo.id,
                        name = labinfo.name,
                        equipment = labinfo.equip,
                        maximum = equipment.maximum
                    });
            var maxi = data.Select(a => a.maximum).FirstOrDefault();
            var pull_data = db.Reserveinfo
                .Where(x => (x.start_time.Day - dateNow.Day) >= 0 &&
                            (x.start_time.Day - dateNow.Day) <= 6 &&
                            x.lab_id == lab_id
                ).OrderBy(x => x.lab_id).OrderBy(x => x.id);
            List<Reserve_page> days = new List<Reserve_page>();
            for (var day = 0; day < 7; day++)
            {
                var dayN = from x in pull_data where x.start_time.Day - dateNow.Day == day select x;
                var temp_day = new Reserve_page { day = day, maximum = maxi };
                List<int> timeslot = new List<int>();
                foreach (var time in time_slot)
                {
                    var n_time = from x in dayN where x.start_time.Hour == time select x;
                    timeslot.Add(n_time.Count());
                }

                temp_day.timeslot = timeslot.ToArray();
                days.Add(temp_day);
            }
            return new Return
            {
                Error = false,
                Data = days.ToArray()
            };
        }

        public Return CancelList(Reserved data)
        {
            var dateNow = DateTime.Now;
            var temp = db.Reserveinfo
                .Where(
                    x => x.lab_id == data.lab_id &&
                        x.start_time.Day == dateNow.Day + data.day &&
                        x.start_time.Hour == time_slot[data.time]
                )
                .Join(
                    db.Userinfo,
                    reserveinfo => reserveinfo.reserve_by,
                    userinfo => userinfo.id,
                    (reserveinfo, userinfo) => new WhoReserved
                    {
                        reserved_id = reserveinfo.id,
                        email = userinfo.email,
                        user_id = userinfo.id
                    }
                ).ToArray();
            string name = db.Labinfo.Where(L => L.id == data.lab_id).First().name;
            var lab_info = db.Reserveinfo.Where(
                x => x.lab_id == data.lab_id &&
                x.start_time.Day == dateNow.Day + data.day &&
                x.start_time.Hour == time_slot[data.time]
            )
            .Join(db.Labinfo, R => R.lab_id, L => L.id,
                (reserveinfo, labinfo) => new LabShowForCancel
                {
                    data = temp,
                    lab_id = data.lab_id,
                    name = name,
                    start_time = reserveinfo.start_time.Hour,
                    end_time = reserveinfo.start_time.Hour + 1,
                    day = reserveinfo.start_time
                }
            );
            return new Return
            {
                Error = false,
                Data = lab_info.ToArray()[0]
            };
        }

        public Return BlackListInfo()
        {

            var wasBlock = db.Userinfo
                .Join(db.Blacklist, a => a.id, b => b.user_id,
                    (userinfo, blacklist) => new UserEmail
                    {
                        email = userinfo.email,
                        user_id = userinfo.id
                    }
                );
            var NotBlock = db.Userinfo
                .Join(db.Userinfo, a => a.id, b => b.id,
                    (userinfo, userinfo1) => new UserEmail
                    {
                        email = userinfo.email,
                        user_id = userinfo.id
                    }
                ).Where(n => wasBlock.Any(w => n.user_id != w.user_id));
            BlackListPage output = new BlackListPage { wasBlock = wasBlock.ToArray(), NotBlock = NotBlock.ToArray() };
            Console.WriteLine(JsonConvert.SerializeObject(output, Formatting.Indented));
            return new Return
            {
                Error = false,
                Data = output
            };
        }

        public Return UnBlock(int userid)
        {
            var check = db.Blacklist.Where(a => a.user_id == userid).FirstOrDefault();
            if (check != null)
            {
                db.Blacklist.Remove(check);
            }
            else
            {
                return new Return
                {
                    Error = true,
                    Data = "ไม่สามารถทำงานได้ กรุณา refresh"
                };
            }
            db.SaveChanges();
            return new Return
            {
                Error = false,
                Data = ""
            };
        }

        public Return ForceBlock(int userid)
        {
            var check = db.Blacklist.Where(a => a.user_id == userid).FirstOrDefault();
            if (check == null)
            {
                db.Blacklist.Add(new Blacklist { user_id = userid });
            }
            else
            {
                return new Return
                {
                    Error = true,
                    Data = "ผู้ใช้นี้ถูก block อยู่่แล้ว"
                };
            }
            db.SaveChanges();
            return new Return
            {
                Error = false,
                Data = ""
            };
        }

        public Return GetEmailUser(int userid)
        {
            var temp = db.Userinfo.Where(a => a.id == userid).First();
            UserEmail output = new UserEmail { email = temp.email, user_id = temp.id };
            return new Return
            {
                Error = false,
                Data = output
            };
        }
    }
}