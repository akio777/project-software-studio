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

        public DbSet<User> User { get; set; }
        public DbSet<Toolinfo> Toolinfo { get; set; }
        public DbSet<Labstore> Labstore { get; set; }
        public DbSet<Labinfo> Labinfo { get; set; }
        public DbSet<Reserve> Reserve { get; set; }
    }
}