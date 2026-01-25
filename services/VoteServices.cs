using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Contracts.Vote;
using SurvayBucketsApi.Entites;
using SurvayBucketsApi.Errors;

namespace SurvayBucketsApi.services;

public class VoteServices(ApplicationDbContext context) : IVoteServices
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result> AddAsync(int pollId, string userid, VoteRequest request, CancellationToken cancellationToken = default)
    {
        var HasVote = await _context.Votes.AnyAsync(v => v.PollId == pollId && v.UserId == userid, cancellationToken);
        if (HasVote)
            return Result.Fail(VoteError.UserAlreadyVoted);


        var PollExsit = await _context.polls.AnyAsync(p => p.Id == pollId && p.IsPublished && p.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow)
                 && p.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken);

        if (!PollExsit)
            return Result.Fail(PollError.PollNotFound);
        var avalibalidAnswers = await _context.Questions.Where(q => q.PollId == pollId && q.IsActive)
            .Select(q => q.Id)
            .ToListAsync(cancellationToken);

        if (!request.Answers.Select(x => x.QuestionId).SequenceEqual(avalibalidAnswers))
            return Result.Fail(VoteError.InvalidAnswers);

        var vote = new Vote
        {
            PollId = pollId,
            UserId = userid,
            VoteAnswers = request.Answers.Adapt<IEnumerable<VoteAnswer>>().ToList(),

        };
        await _context.AddAsync(vote, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}
