using JuniorWeb.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorWeb.DetaAccess.Configurations
{
    public class PatientConfiguration : EntityConfiguration<Patient>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Patient> builder)
        {

            builder.HasIndex(x => x.FirstName);
            builder.HasIndex(x => x.LastName);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(10);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(20);
            builder.Property(x => x.JMBG).IsRequired().HasMaxLength(13);

            builder.HasOne(x => x.Hospital)
                    .WithMany(x => x.Patients).HasForeignKey(x => x.HospitalId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Diagnosis)
                    .WithMany(x => x.Patients);
            builder.HasOne(x => x.Room).WithMany(x => x.Patients).HasForeignKey(x => x.RoomId);
            builder.HasOne(x => x.Doctor).WithMany(x => x.Patients).HasForeignKey(x => x.DoctorId);

        }
    }
}
