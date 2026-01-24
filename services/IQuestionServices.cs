using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Contracts.Common;
using SurvayBucketsApi.Contracts.Question;

namespace SurvayBucketsApi.services;
public interface IQuestionServices
{
    Task<Result<QuestionResponse>> AddAsync( int pollId ,  QuestionRequest request , CancellationToken cancellationToken);
    Task<Result<PaginatedList<QuestionResponse>>> GetAllAsync(int pollId, RequestFilter filter, CancellationToken cancellationToken);    Task<Result<IEnumerable<QuestionResponse>>> GetAvilabelAsync(int pollId, string userid ,  CancellationToken cancellationToken);
    Task<Result<QuestionResponse>> GetAsync(int pollId, int id ,  CancellationToken cancellationToken);
    Task<Result> UpdateAsync(int pollId, int id , QuestionRequest Request  ,  CancellationToken cancellationToken);

    Task<Result> ToggleStatus([FromRoute] int pollid, [FromRoute] int id, CancellationToken cancellation);

}