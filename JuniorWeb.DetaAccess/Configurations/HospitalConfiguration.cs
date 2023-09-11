using JuniorWeb.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorWeb.DetaAccess.Configurations
{
    public class HospitalConfiguration : EntityConfiguration<Hospital>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Hospital> builder)
        {
            builder.Property(x => x.Location).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);

            builder.HasIndex(x => x.Name);

            builder.Property(x => x.Covid19).IsRequired().HasDefaultValue(false);

            builder.HasOne(x => x.HospitalType).WithOne(x => x.Hospital);
            builder.HasMany(x => x.Rooms).WithOne(x => x.Hospital);
            builder.HasMany(x => x.Patients).WithOne(x => x.Hospital);
        }
    }
}
