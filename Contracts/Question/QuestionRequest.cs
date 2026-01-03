using SurvayBucketsApi.Contracts.Answers;
using SurvayBucketsApi.Entites;

namespace SurvayBucketsApi.Contracts.Question;

public record QuestionRequest(
    
    string Content,
    List<string> Answers 

    );

