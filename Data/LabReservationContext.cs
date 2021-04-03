using Microsoft.EntityFrameworkCore;
using LabReservation.Models;

namespace LabReservation.Data
{
    public class LabReservationContext : DbContext
    {
        public LabReservationContext (DbContextOptions<LabReservationContext> options)
            : base(options)
        {
        }

        public DbSet<Userinfo> Userinfo { get; set; }
        public DbSet<Reserveinfo> Reserveinfo { get; set; }
        public DbSet<Labinfo> Labinfo { get; set; }
        public DbSet<Blacklist> Blacklist { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
    }
}