using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Domain.Core.Entities;

namespace Theater.Infrastructure.Data.Configurations
{
    class PerformanceConfiguration : IEntityTypeConfiguration<Performance>
    {
        public void Configure(EntityTypeBuilder<Performance> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).
                IsRequired().
                HasMaxLength(50);

            builder.Property(p => p.Genre).
                IsRequired().
                HasMaxLength(20);

            builder.Property(p => p.Audience).
                HasMaxLength(20);
        }
    }
}
