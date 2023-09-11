using JuniorWeb.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorWeb.DetaAccess.Configurations
{
    public class SpecializationConfiguration : EntityConfiguration<Specialization>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Specialization> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            builder.HasIndex(x => x.Name);

            builder.HasMany(x => x.Doctors).WithOne(x => x.Specialization);
        }
    }
}
