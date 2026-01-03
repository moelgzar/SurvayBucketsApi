using SurvayBucketsApi.Entites;

namespace SurvayBucketsApi.Contracts.Vote;

public record VoteAnswerRequest(

    int QuestionId,
    int AnswerId

    );


