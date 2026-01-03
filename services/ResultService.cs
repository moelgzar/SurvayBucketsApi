using Microsoft.EntityFrameworkCore;
using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Contracts.Question;
using SurvayBucketsApi.Contracts.Results;
using SurvayBucketsApi.Errors;

namespace SurvayBucketsApi.services;

public class ResultService(ApplicationDbContext context) : IResultService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<PollVoteResponse>> ResultsAsync(int pollId, CancellationToken cancellationToken = default)
    {

        var poll = await _context.polls
            .Where(p => p.Id == pollId)
            .Select(p => new PollVoteResponse(
                p.Title,
                p.Votes.Select(x => new ResultVoteResponse(
                    $"{x.User.FirstName} {x.User.LastName}",
                    x.SubmittedOn,
                    x.VoteAnswers.Select(a => new ResultAnswerQuestionResponse(
                        a.Question.Content,
                        a.Answer.Content
                    ))
                ))
            ))
            .FirstOrDefaultAsync(cancellationToken);


        //if(poll is null)
        //    return Result.Fail<PollVoteResponse>(PollError.PollNotFound);
        //return Result.Success(poll);

        return poll is null ? Result.Fail<PollVoteResponse>(PollError.PollNotFound)
            : Result.Success(poll);
    }

    public async Task<Result<IEnumerable<VotePerDayResponse>>> GetVotesPerDayAsync(int pollId, CancellationToken cancellationToken = default)
    {


      var pollExsit = await _context.polls.AnyAsync(p => p.Id == pollId, cancellationToken);
        if (!pollExsit)
            return Result.Fail<IEnumerable<VotePerDayResponse>>(PollError.PollNotFound);


        var votesPerDay = await _context.Votes
            .Where( x=>x.PollId == pollId)
            .GroupBy(v=> new { Date = DateOnly.FromDateTime(v.SubmittedOn) })
            .Select(g=> new VotePerDayResponse(
                 g.Key.Date,
                 g.Count()
                )
            )
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<VotePerDayResponse>>(votesPerDay);

    }

    public async Task<Result<IEnumerable<VotesPerQuestionResponse>>> GetVotesPerQuestionAsync(int pollId, CancellationToken cancellationToken = default)
    {


        var pollExsit = await _context.polls.AnyAsync(p => p.Id == pollId, cancellationToken);
        if (!pollExsit)
            return Result.Fail<IEnumerable<VotesPerQuestionResponse>>(PollError.PollNotFound);


       
        var votesPerQuestion = await _context.VoteAnswers
            .Where(x=>x.Vote.PollId == pollId)
            .Select(g => new VotesPerQuestionResponse(
               
                g.Question.Content,
                g.Question.Votes.GroupBy( x => new { AnswerId = x.AnswerId , AnswerContent = x.Answer.Content })
                .Select( ag => new VotePerAnswerResponse(
                    ag.Key.AnswerContent,
                    ag.Count()
                 ))
            ))
            .ToListAsync(cancellationToken);


        return Result.Success<IEnumerable<VotesPerQuestionResponse>>(votesPerQuestion);
    }

}
