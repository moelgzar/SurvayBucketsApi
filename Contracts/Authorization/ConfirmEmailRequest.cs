namespace SurvayBucketsApi.Contracts.Authorization;

public record ConfirmEmailRequest(
    string UserId,
    string Code
    );

