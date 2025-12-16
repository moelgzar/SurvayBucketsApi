using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SurvayBucketsApi.Persistence.EntitesCnfigrations;

public class PollConfigration : IEntityTypeConfiguration<Poll>
{
    public void Configure(EntityTypeBuilder<Poll> builder)
    {
        builder.HasIndex(x => x.Title).IsUnique();
        builder.Property(x=>x.Title).HasMaxLength(100);
        builder.Property(x => x.Summray).HasMaxLength(1500);

       
    }
}
