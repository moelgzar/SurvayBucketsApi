namespace SurvayBucketsApi.services;

public interface IPollservice
{
   Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellation);
   Task <Poll?> GetAsync(int id , CancellationToken cancellation);
   Task <Poll> AddPollAsync(Poll poll, CancellationToken cancellation);
    Task<bool> UpdatePollAsync(int id , Poll poll , CancellationToken cancellation);
    Task<bool> togglePublishStatus(int id , CancellationToken cancellation);
    Task<bool> DeletePollAsync(int id , CancellationToken cancellation);
}
