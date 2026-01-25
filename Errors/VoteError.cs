using SurvayBucketsApi.Abstractions;

namespace SurvayBucketsApi.Errors;

public static class VoteError
{
    public static readonly Error UserAlreadyVoted = new("Poll.invalid.Vote", "user already vote in this poll ", StatusCodes.Status409Conflict);
    public static readonly Error InvalidAnswers = new("Poll.invalid.Vote", "user enter  InvalidAnswers in this poll ", StatusCodes.Status400BadRequest);

}