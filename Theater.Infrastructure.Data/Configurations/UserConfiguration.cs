using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Domain.Core.Entities;

namespace Theater.Infrastructure.Data.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {           

            builder.Property(p => p.BirthDate).
                IsRequired();

            builder.Property(p => p.Sex).
                IsRequired().
                HasMaxLength(6);
        }
    }
}
