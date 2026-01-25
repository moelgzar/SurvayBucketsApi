namespace SurvayBucketsApi.Contracts.User;

public record UserProfileResponse(
    string UserName,
    string Email,
    string FirstName,
    string LastName

    );

