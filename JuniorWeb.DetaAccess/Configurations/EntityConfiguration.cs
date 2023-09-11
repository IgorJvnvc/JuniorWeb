using JuniorWeb.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorWeb.DetaAccess.Configurations
{
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.UpdatedBy).HasMaxLength(50);
            builder.Property(x => x.DeletedBy).HasMaxLength(50);

            ConfigureRules(builder);
        }

        protected abstract void ConfigureRules(EntityTypeBuilder<T> builder);
    }
}
