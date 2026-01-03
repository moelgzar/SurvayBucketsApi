namespace SurvayBucketsApi.Contracts.Results;

public record ResultVoteResponse(
    string VoterName,
    DateTime VotedDate,
    IEnumerable<ResultAnswerQuestionResponse> SelectedAnswers
    );
