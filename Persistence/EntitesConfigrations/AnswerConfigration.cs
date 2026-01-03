using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurvayBucketsApi.Entites;

namespace SurvayBucketsApi.Persistence.EntitesCnfigrations;

public class AnswerConfigration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.HasIndex(x => new  { x.Id , x.Content }).IsUnique();
        builder.Property(x=>x.Content).HasMaxLength(1000);

       
    }


    

}
