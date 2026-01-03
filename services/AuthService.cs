using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Authorization;
using SurvayBucketsApi.Contracts.Authorization;
using SurvayBucketsApi.Entites;
using SurvayBucketsApi.Errors;

namespace SurvayBucketsApi.services;

public class AuthService(UserManager<ApplicationUser> userManager , IJwtProvider jwtProvider) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    private IJwtProvider _JwtProvider = jwtProvider;

    private readonly int _RefreshTokenExpiration = 14 ;

    public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellation = default)
    {
        //check email foud 2- password correct , 3- return token 

        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return Result.Fail<AuthResponse>(UserError.UserCredintial);

        var IsValidPassword = await _userManager.CheckPasswordAsync(user, password);

        if (!IsValidPassword)
            return Result.Fail<AuthResponse>(UserError.UserCredintial);

        var (token , expirein ) = _JwtProvider.GeneratedToken(user);

        var RefreshToken = GenerateRefreshToken(); 

        var RefreshTokenExpirDate = DateTime.UtcNow.AddDays(_RefreshTokenExpiration);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = RefreshToken,
            ExpireDate = RefreshTokenExpirDate,

        }
            );

        await _userManager.UpdateAsync(user);

        var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expirein, RefreshToken, RefreshTokenExpirDate);


        return  Result.Success(response);

    }

   private static string  GenerateRefreshToken()
    {

        return Convert.ToBase64String (RandomNumberGenerator.GetBytes(64));
    }

    public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, CancellationToken cancellation = default)

    {

        var userId = _JwtProvider.ValidateToken(token);

        if (userId is null)
            return Result.Fail<AuthResponse>(UserError.UserCredintial);

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result.Fail<AuthResponse>(UserError.UserID);

        var userrefreeshtoken = user.RefreshTokens.SingleOrDefault(t => t.Token == token && t.IsActive());
        if (userrefreeshtoken is null)
            return Result.Fail<AuthResponse>(UserError.UserRefreshTokenNotFound);

        var (newtoken, expirein) = _JwtProvider.GeneratedToken(user);

        var newRefreshToken = GenerateRefreshToken();

        var RefreshTokenExpirDate = DateTime.UtcNow.AddDays(_RefreshTokenExpiration);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpireDate = RefreshTokenExpirDate,

        }
            );

        await _userManager.UpdateAsync(user);


        var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, newtoken, expirein, newRefreshToken, RefreshTokenExpirDate);

        return Result.Success(response);


    }

    public async Task<bool> RevokeRefreshTokenAsync(string token, string refreshToken ,  CancellationToken cancellation = default)

    {

        var userId = _JwtProvider.ValidateToken(token);

        if (userId is null)
            return false;

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return false;

        var userrefreeshtoken = user.RefreshTokens.SingleOrDefault(t => t.Token == refreshToken && t.IsActive());
        if (userrefreeshtoken is null)
            return false;


        userrefreeshtoken.RevokeOn = DateTime.UtcNow;
        
        await _userManager.UpdateAsync(user);

        return true;

    }
}
