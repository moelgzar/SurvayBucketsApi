namespace SurvayBucketsApi.Contracts.Register;

public record RegisterRequestShape(
    string Email,
    string Password,
    string FirstName,
    string LastName
    );

