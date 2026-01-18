namespace SurvayBucketsApi.Contracts.User;

public record ChangePasswordRequest(
    
    string Currentpassword,
    string Newpassword
    );
