using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Domain.Core.Models;

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
        }
    }
}
