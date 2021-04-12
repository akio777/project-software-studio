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
                // Look for any movies.
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
                        new Equipment{id = 1, lab_id = 1, maximum = 10},
                        new Equipment{id = 2, lab_id = 2, maximum = 20},
                        new Equipment{id = 3, lab_id = 3, maximum = 30},
                        new Equipment{id = 4, lab_id = 4, maximum = 40},
                        new Equipment{id = 5, lab_id = 5, maximum = 50}
                    );
                }
                
                
                
                context.SaveChanges();
            }
        }
    }
}