using SurvayBucketsApi.Abstractions;

namespace SurvayBucketsApi.Errors;

public static class QuestionError
{
    public static readonly Error QuestionNotFound = new("Question.invalid", "Question Not found ", StatusCodes.Status404NotFound);


    public static readonly Error QuestionDuplcated = new("Poll.Duplicated.Question", "Question exsit before in this poll ", StatusCodes.Status409Conflict);

}
