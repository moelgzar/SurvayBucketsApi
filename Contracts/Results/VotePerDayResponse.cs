namespace SurvayBucketsApi.Contracts.Results;

public record VotePerDayResponse(
    DateOnly Date,
    int NumberOfVotes
    );

