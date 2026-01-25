using SurvayBucketsApi.Entites;
public class Poll : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Summray { get; set; }
    public bool IsPublished { get; set; }
    public DateOnly StartsAt { get; set; }
    public DateOnly EndsAt { get; set; }
    public ICollection<Question> Questions { get; set; } = [];
    public ICollection<Vote> Votes { get; set; } = [];

}
