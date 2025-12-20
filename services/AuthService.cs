using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using SurvayBucketsApi.Authorization;
using SurvayBucketsApi.Contracts.Authorization;
using SurvayBucketsApi.Entites;

namespace SurvayBucketsApi.services;

public class AuthService(UserManager<ApplicationUser> userManager , IJwtProvider jwtProvider) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    private IJwtProvider _JwtProvider = jwtProvider;

    private readonly int _RefreshTokenExpiration = 14 ;

    public async Task<AuthResponse> GetTokenAsync(string email, string password, CancellationToken cancellation = default)
    {
        //check email foud 2- password correct , 3- return token 

        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return null;

        var IsValidPassword = await _userManager.CheckPasswordAsync(user, password);

        if (!IsValidPassword)
            return null;

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




        return new AuthResponse( user.Id, user.Email, user.FirstName, user.LastName, token, expirein , RefreshToken , RefreshTokenExpirDate);

    }

   private static string  GenerateRefreshToken()
    {

        return Convert.ToBase64String (RandomNumberGenerator.GetBytes(64));
    }

    public async Task<AuthResponse> GetRefreshTokenAsync(string token, CancellationToken cancellation = default)

    {

        var userId = _JwtProvider.ValidateToken(token);

        if (userId is null)
            return null;

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return null;

        var userrefreeshtoken = user.RefreshTokens.SingleOrDefault(t => t.Token == token && t.IsActive());
        if (userrefreeshtoken is null)
            return null;

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



        return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, newtoken, expirein, newRefreshToken, RefreshTokenExpirDate);


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
