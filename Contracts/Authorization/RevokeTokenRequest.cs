namespace SurvayBucketsApi.Contracts.Authorization;

public record RevokeTokenRequest(
    string Token,
    string RefresheToken

    );

