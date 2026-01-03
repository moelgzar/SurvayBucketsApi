using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Contracts.Vote;

namespace SurvayBucketsApi.services;

public interface IVoteServices
{
    Task<Result> AddAsync(int pollId, string userid ,  VoteRequest request ,  CancellationToken cancellationToken = default);
}
