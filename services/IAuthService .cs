using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Contracts.Authorization;

namespace SurvayBucketsApi.services;

public interface IAuthService
{
    Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellation = default);
    Task<Result<AuthResponse>> GetRefreshTokenAsync(string token , CancellationToken cancellation = default);

    Task<bool> RevokeRefreshTokenAsync(string token , string refreshToken, CancellationToken cancellation = default);


}
