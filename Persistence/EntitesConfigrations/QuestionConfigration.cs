using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurvayBucketsApi.Entites;

namespace SurvayBucketsApi.Persistence.EntitesCnfigrations;

public class QuestionConfigration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasIndex(x => new { x.PollId, x.Content }).IsUnique();
        builder.Property(x => x.Content).HasMaxLength(1000);


    }




}
