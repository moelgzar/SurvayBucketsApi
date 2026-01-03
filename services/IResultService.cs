using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Contracts.Results;

namespace SurvayBucketsApi.services;

public interface IResultService
{
    Task<Result<PollVoteResponse>> ResultsAsync(int pollId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<VotePerDayResponse>>> GetVotesPerDayAsync(int pollId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<VotesPerQuestionResponse>>> GetVotesPerQuestionAsync(int pollId, CancellationToken cancellationToken = default);
}
