using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Domain.Core.Entities;

namespace Theater.Infrastructure.Data.Configurations
{
    class PosterConfiguration : IEntityTypeConfiguration<Poster>
    {
        public void Configure(EntityTypeBuilder<Poster> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.DateTime).
                IsRequired();

            builder.Property(p => p.Premiere).
                IsRequired();
        }
    }
}
