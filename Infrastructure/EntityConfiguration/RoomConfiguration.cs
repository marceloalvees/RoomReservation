using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Capacity).IsRequired();
            builder.Property(r => r.Location).IsRequired().HasMaxLength(100);

            builder.HasData(
                new Room { Id = 1, Name = "Room 1", Capacity = 20, Location = "Location 1" },
                new Room { Id = 2, Name = "Room 2", Capacity = 30, Location = "Location 2" },
                new Room { Id = 3, Name = "Room 3", Capacity = 25, Location = "Location 3" },
                new Room { Id = 4, Name = "Room 4", Capacity = 15, Location = "Location 4" },
                new Room { Id = 5, Name = "Room 5", Capacity = 50, Location = "Location 5" },
                new Room { Id = 6, Name = "Room 6", Capacity = 10, Location = "Location 6" },
                new Room { Id = 7, Name = "Room 7", Capacity = 35, Location = "Location 7" },
                new Room { Id = 8, Name = "Room 8", Capacity = 40, Location = "Location 8" },
                new Room { Id = 9, Name = "Room 9", Capacity = 20, Location = "Location 9" },
                new Room { Id = 10, Name = "Room 10", Capacity = 60, Location = "Location 10" }
            );
        }
    }
}
