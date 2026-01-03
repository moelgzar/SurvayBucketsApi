namespace SurvayBucketsApi.Entites;

public class Vote
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int PollId { get; set; }

    public DateTime SubmittedOn { get; set; } = DateTime.UtcNow;
    public ICollection<VoteAnswer> VoteAnswers { get; set; } = [];
    public ApplicationUser User { get; set; } = default!;
    public Poll Poll { get; set; } = default!;

}
