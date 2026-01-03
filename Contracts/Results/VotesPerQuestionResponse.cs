namespace SurvayBucketsApi.Contracts.Results;

public record VotesPerQuestionResponse(
    string Question,
    IEnumerable<VotePerAnswerResponse> SelectedAnswer
    );

