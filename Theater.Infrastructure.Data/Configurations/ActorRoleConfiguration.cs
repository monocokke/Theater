using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Domain.Core.Entities;

namespace Theater.Infrastructure.Data.Configurations
{
    class ActorRoleConfiguration : IEntityTypeConfiguration<ActorRole>
    {
        public void Configure(EntityTypeBuilder<ActorRole> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasAlternateKey(p => new { p.ActorId, p.RoleId });

            builder.Property(p => p.Understudy).
                IsRequired();
        }
    }
}
