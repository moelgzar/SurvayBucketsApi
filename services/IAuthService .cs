using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Contracts.Authorization;
using SurvayBucketsApi.Contracts.Register;

namespace SurvayBucketsApi.services;

public interface IAuthService
{
    Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellation = default);
    Task<Result<AuthResponse>> GetRefreshTokenAsync(string token , CancellationToken cancellation = default);

    Task<bool> RevokeRefreshTokenAsync(string token , string refreshToken, CancellationToken cancellation = default);

    Task<Result> RegisterAsync(RegisterRequestShape request, CancellationToken cancellation = default);
    Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request);
    Task<Result> ResendConfirmEmailAsync(ResendConfirmEmail request);
    Task<Result> SendResetPasswordCodeAsync(ForgetPasswordRequest request);
    Task<Result> ResetPasswordAsync(ResetPasswordRequest request);
}
