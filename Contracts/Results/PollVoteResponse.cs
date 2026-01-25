namespace SurvayBucketsApi.Contracts.Results;

public record PollVoteResponse(

    string Title,

    IEnumerable<ResultVoteResponse> Votes

    );
