using SurvayBucketsApi.Abstractions;

namespace SurvayBucketsApi.services;

public interface IPollservice
{
    Task<IEnumerable<PollResponse>> GetAllAsync(CancellationToken cancellation);
    Task<IEnumerable<PollResponse>> GetCurrentAsync(CancellationToken cancellation);
    Task<IEnumerable<PollResponseV2>> GetCurrentAsyncV2(CancellationToken cancellation = default);

    Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellation);
    Task<Result<PollResponse>> AddPollAsync(PollRequest request, CancellationToken cancellation);
    Task<Result> UpdatePollAsync(int id, PollRequest request, CancellationToken cancellation);
    Task<Result> togglePublishStatus(int id, CancellationToken cancellation);
    Task<Result> DeletePollAsync(int id, CancellationToken cancellation);
}
