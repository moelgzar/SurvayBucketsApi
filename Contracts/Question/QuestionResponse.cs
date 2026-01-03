using SurvayBucketsApi.Contracts.Answers;

namespace SurvayBucketsApi.Contracts.Question;

public record QuestionResponse(
    
    int id ,
    string Content, 
    IEnumerable<AnswerResponse> Answers

    );

