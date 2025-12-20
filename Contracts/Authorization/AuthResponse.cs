namespace SurvayBucketsApi.Contracts.Authorization;

public record AuthResponse(
string Id ,
string? Email,
string FirstName,
string LasttName,
string token,
int ExperiesIn,
string RefreshToken, 
DateTime RefreshTokenExpiration

    
    );
