using System;
using System.Linq;

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
        public LabService(LabReservationContext context)
        {
            db = context;
        }

        public Return Read()
        {
            var temp = db.Labinfo
                .Join(
                    db.Equipment,
                    labinfo => labinfo.id,
                    equipment => equipment.lab_id,
                    (labinfo, equipment) => new
                    {
                        LabId = labinfo.id,
                        Equipment = labinfo.equip,
                        amount = equipment.maximum
                    }
                ).ToList();

            foreach (var i in temp)
            {
                Console.WriteLine(i);
            }
            
            return new Return
            {
                Error = false,
                Data = "xxxxx"
            };
        }
    }
}