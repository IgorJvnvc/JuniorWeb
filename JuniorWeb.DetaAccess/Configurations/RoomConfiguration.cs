using JuniorWeb.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorWeb.DetaAccess.Configurations
{
    public class RoomConfiguration : EntityConfiguration<Room>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Room> builder)
        {
            builder.Property(x => x.Beds).IsRequired();
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.Floor).IsRequired();
            builder.Property(x => x.ForWhatTypeOfPatient)
                    .IsRequired()
                    .HasMaxLength(20);
            builder.Property(x => x.IsFull).IsRequired().HasDefaultValue(false);

            builder.HasIndex(x => x.Number);
            builder.HasIndex(x => x.ForWhatTypeOfPatient);

            builder.HasOne(x => x.Hospital).WithMany(x => x.Rooms).HasForeignKey(x => x.HospitalId);
            builder.HasMany(x => x.Patients).WithOne(x => x.Room);
            ;
        }
    }
}
