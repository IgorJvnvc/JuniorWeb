using JuniorWeb.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorWeb.DetaAccess.Configurations
{
    public class DoctorConfiguration : EntityConfiguration<Doctor>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasIndex(x => x.LastName);
            builder.Property(x => x.FristName).HasMaxLength(10).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(20).IsRequired();

            builder.HasMany(x => x.Diagnosiss).WithMany(x => x.Doctors);

        }

    }
}
