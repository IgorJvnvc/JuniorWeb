using JuniorWeb.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorWeb.DetaAccess.Configurations
{
    public class DiagnosisConfiguration : EntityConfiguration<Diagnosis>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Diagnosis> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.DiagnosedAt).IsRequired();

            builder.HasIndex(x => x.DiagnosedAt);

            builder.HasMany(x => x.Doctors)
                   .WithMany(x => x.Diagnosiss);

            builder.HasMany(x => x.Patients)
                .WithMany(x => x.Diagnosis);

        }
    }
}
