using JuniorWeb.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorWeb.DetaAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email).HasMaxLength(40).IsRequired();
            builder.Property(x => x.Username).HasMaxLength(40).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(40).IsRequired();

            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
