using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LabReservation.Data;
using System;
using System.Linq;

namespace LabReservation.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LabReservationContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LabReservationContext>>()))
            {
                Random r = new Random();
                var dateNow = DateTime.Now;
                // Look for any movies.

                if (!context.Userinfo.Any())
                    context.Userinfo.Add(new Userinfo{email = "admin@admin.com",id = 1,password = "123456",role = 0});
                
                if (!context.Labinfo.Any())
                {
                    context.Labinfo.AddRange(
                        new Labinfo
                        {
                            id = 1,
                            name = "LAB001",
                            equip = "TOOL-1"
                        },
                        new Labinfo
                        {
                            id = 2,
                            name = "LAB002",
                            equip = "TOOL-2"
                        },
                        new Labinfo
                        {
                            id = 3,
                            name = "LAB003",
                            equip = "TOOL-3"
                        },
                        new Labinfo
                        {
                            id = 4,
                            name = "LAB004",
                            equip = "TOOL-4"
                        },
                        new Labinfo
                        {
                            id = 5,
                            name = "LAB005",
                            equip = "TOOL-5"
                        }
                    );
                }

                if (!context.Equipment.Any())
                {
                    context.Equipment.AddRange(
                        new Equipment { id = 1, lab_id = 1, maximum = 10 },
                        new Equipment { id = 2, lab_id = 2, maximum = 20 },
                        new Equipment { id = 3, lab_id = 3, maximum = 30 },
                        new Equipment { id = 4, lab_id = 4, maximum = 40 },
                        new Equipment { id = 5, lab_id = 5, maximum = 50 }
                    );
                }

                Console.WriteLine("SEED DATA");
                // for (var i = 0; i < 10; i++)
                // {
                //     var random = r.Next(8, 17);

                //     context.Reserveinfo.AddRange(
                //         new Reserveinfo
                //         {
                //             lab_id = 1,
                //             reserve_by = r.Next(1, 20),
                //             start_time = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day + r.Next(1, 7), random + 1, 0, 0),
                //             end_time = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day + r.Next(1, 7), random + 2, 0, 0),
                //         }
                // ,
                // new Reserveinfo
                // {
                //     lab_id = r.Next(1, 6),
                //     reserve_by = r.Next(1, 20),
                //     start_time = new DateTime(dateNow.Year, dateNow.Month, r.Next(1, 28), random + 2, 0, 0),
                //     end_time = new DateTime(dateNow.Year, dateNow.Month, r.Next(1, 28), random + 3, 0, 0),
                // },
                // new Reserveinfo
                // {
                //     lab_id = r.Next(1, 6),
                //     reserve_by = r.Next(1, 20),
                //     start_time = new DateTime(dateNow.Year, dateNow.Month, r.Next(1, 28), random + 3, 0, 0),
                //     end_time = new DateTime(dateNow.Year, dateNow.Month, r.Next(1, 28), random + 4, 0, 0),
                // }
                // );
                // }
                context.SaveChanges();
            }
        }
    }
}