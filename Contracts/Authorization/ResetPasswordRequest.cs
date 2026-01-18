namespace SurvayBucketsApi.Contracts.Authorization;

public record ResetPasswordRequest(
    string Email ,
    string Code ,
    string NewPassword
    
    
    );
