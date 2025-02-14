using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Email).IsRequired().HasMaxLength(100);
            builder.Property(r => r.PhoneNumber).IsRequired().HasMaxLength(15);
        }
    }
}
