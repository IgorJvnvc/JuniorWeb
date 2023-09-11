using JuniorWeb.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorWeb.DetaAccess.Configurations
{
    public class Hospitaltypeconfiguration : EntityConfiguration<HospitalType>
    {
        protected override void ConfigureRules(EntityTypeBuilder<HospitalType> builder)
        {
            builder.Property(x => x.Private).IsRequired().HasDefaultValue(false);
            builder.HasOne(x => x.Hospital).WithOne(x => x.HospitalType);
        }
    }
}
