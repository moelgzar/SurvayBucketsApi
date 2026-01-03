namespace SurvayBucketsApi.Contracts.Vote;

public record VoteRequest(
    
    IEnumerable<VoteAnswerRequest> Answers
    
    );
