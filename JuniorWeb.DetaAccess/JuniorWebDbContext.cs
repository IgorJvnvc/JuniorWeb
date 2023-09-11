using JuniorWeb.Domain;
using Microsoft.EntityFrameworkCore;

namespace JuniorWeb.DetaAccess
{
    public class JuniorWebDbContext : DbContext
    {
        public JuniorWebDbContext(DbContextOptions options=null ) : base(options)
        {
            
        }
        //public IApplicationUser User { get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.Entity<Diagnosis>().HasKey(x => new { x.DoctorId, x.PatientId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<HospitalType> HospitalTypes { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<User> Users { get; set; }
    }


}
