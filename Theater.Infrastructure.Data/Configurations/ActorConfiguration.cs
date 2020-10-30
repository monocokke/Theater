using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Domain.Core.Models;

namespace Theater.Infrastructure.Data.Configurations
{
    class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.User).
                WithOne(p => p.Actor).
                HasForeignKey<Actor>(p => p.UserId);

            builder.Property(p => p.UserId).
                IsRequired();

            builder.Property(p => p.EyeColor).
                IsRequired().
                HasMaxLength(10);

            builder.Property(p => p.HairColor).
                IsRequired().
                HasMaxLength(10);

            builder.Property(p => p.Nationality).
                IsRequired().
                HasMaxLength(10);

            builder.Property(p => p.Height).
                IsRequired();
        }
    }
}
