using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Domain.Core.Entities;

namespace Theater.Infrastructure.Data.Configurations
{
    class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).
                IsRequired().
                HasMaxLength(30);

            builder.Property(p => p.Sex).
                IsRequired().
                HasMaxLength(6);

            builder.Property(p => p.Description).
                HasMaxLength(1000);

            builder.Property(p => p.PerformanceId).
                IsRequired();

            builder.Property(p => p.EyeColor).
                HasMaxLength(10);

            builder.Property(p => p.HairColor).
                HasMaxLength(10);

            builder.Property(p => p.Nationality).
                HasMaxLength(10);
        }
    }
}
