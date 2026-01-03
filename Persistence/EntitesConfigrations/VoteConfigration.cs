using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurvayBucketsApi.Entites;

namespace SurvayBucketsApi.Persistence.EntitesCnfigrations;

public class VoteConfigration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.HasIndex(x => new  { x.UserId , x.PollId }).IsUnique();

       
    }
 

}
