namespace SurvayBucketsApi.Contracts.Results;

public record VotePerAnswerResponse(
    string Answer,
    int NumberOfVotes
    );

