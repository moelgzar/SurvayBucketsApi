using SurvayBucketsApi.Contracts.Authorization;

namespace SurvayBucketsApi.services;

public interface IAuthService
{
    Task<AuthResponse> GetTokenAsync(string email, string password, CancellationToken cancellation = default);
    Task<AuthResponse> GetRefreshTokenAsync(string token , CancellationToken cancellation = default);

    Task<bool> RevokeRefreshTokenAsync(string token , string refreshToken, CancellationToken cancellation = default);


}
