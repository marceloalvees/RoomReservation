using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Date).IsRequired();
            builder.Property(r => r.Status).IsRequired();
            builder.Property(r => r.UserId).IsRequired();
            builder.Property(r => r.RoomId).IsRequired();
            builder.Property(r => r.DateCancelation);
        }
    }
}
