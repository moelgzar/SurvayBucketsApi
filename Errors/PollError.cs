using SurvayBucketsApi.Abstractions;

namespace SurvayBucketsApi.Errors;

public static class PollError
{
    public static readonly Error PollNotFound = new("Poll.invalid", "Poll Not found ", StatusCodes.Status404NotFound);

    public static readonly Error PollNotCreated = new("Poll.invalid", "Poll Not Created ", StatusCodes.Status404NotFound);

    public static readonly Error PollDuplcated = new("Poll.Duplicated", "Poll exsit before ", StatusCodes.Status409Conflict);
}