using System.Security.Cryptography;
using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Abstractions.Const;
using SurvayBucketsApi.Authorization;
using SurvayBucketsApi.Contracts.Authorization;
using SurvayBucketsApi.Contracts.Register;
using SurvayBucketsApi.Entites;
using SurvayBucketsApi.Errors;
using SurvayBucketsApi.Helpers;

namespace SurvayBucketsApi.services;

public class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
    IJwtProvider jwtProvider, ILogger<ApplicationUser> logger, IHttpContextAccessor httpContextAccessor,
    IEmailSender emailSender, ApplicationDbContext context) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private IJwtProvider _JwtProvider = jwtProvider;
    private readonly ILogger _logger = logger;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly ApplicationDbContext _context = context;
    private readonly int _RefreshTokenExpiration = 14;

    public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellation = default)
    {
        //check email foud 2- password correct , 3- return token 

        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return Result.Fail<AuthResponse>(UserError.UserCredintial);

        if (user.IsDisabled)
            return Result.Fail<AuthResponse>(UserError.UserDisabled);



        //Select roles 


        var (UserRoles, UserPermission) = await GetRoleAndPermission(user, cancellation);


        var result = await _signInManager.PasswordSignInAsync(user, password, false, true);

        if (result.Succeeded)
        {

            var (token, expirein) = _JwtProvider.GeneratedToken(user, UserRoles, UserPermission);

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


            return Result.Success(response);

        }

        var error = result.IsNotAllowed ? UserError.EmailNotConfirmed : result.IsLockedOut ? UserError.LockedUser : UserError.UserCredintial;


        return Result.Fail<AuthResponse>(error);


    }


    private static string GenerateRefreshToken()
    {

        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, CancellationToken cancellation = default)

    {

        var userId = _JwtProvider.ValidateToken(token);

        if (userId is null)
            return Result.Fail<AuthResponse>(UserError.UserCredintial);

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result.Fail<AuthResponse>(UserError.UserID);


        if (user.IsDisabled)
            return Result.Fail<AuthResponse>(UserError.UserDisabled);

        var userrefreeshtoken = user.RefreshTokens.SingleOrDefault(t => t.Token == token && t.IsActive());
        if (userrefreeshtoken is null)
            return Result.Fail<AuthResponse>(UserError.UserRefreshTokenNotFound);

        var claims = GetRoleAndPermission(user, cancellation);
        var (newtoken, expirein) = _JwtProvider.GeneratedToken(user, claims.Result.roles, claims.Result.permission);

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

    public async Task<bool> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellation = default)

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
    public async Task<Result> RegisterAsync(RegisterRequestShape request, CancellationToken cancellation = default)
    {

        var IsemailExits = await _userManager.Users.AnyAsync(u => u.Email == request.Email, cancellation);
        if (IsemailExits)
            return Result.Fail<AuthResponse>(UserError.UserEmailExits);

        var user = request.Adapt<ApplicationUser>();


        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Email confirmation code for user: {Code} ", code);



            //send email

            await SendConfirmationEmail(user, code);



            return Result.Success();
        }

        var error = result.Errors.First();
        return Result.Fail<AuthResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

    }

    public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user is null)
            return Result.Fail(UserError.UserID);

        if (user.EmailConfirmed)
            return Result.Fail(UserError.DuplicatConfirmation);


        var code = request.Code;
        try
        {
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));

        }
        catch (FormatException)
        {
            return Result.Fail(UserError.invalidCode);
        }

        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, DefaultRole.Member);
            return Result.Success();
        }
        var error = result.Errors.First();
        return Result.Fail<AuthResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

    }


    public async Task<Result> ResendConfirmEmailAsync(ResendConfirmEmail request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return Result.Fail(UserError.UserNotFound);

        if (user.EmailConfirmed)
            return Result.Fail(UserError.DuplicatConfirmation);

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        _logger.LogInformation("Email confirmation code for user: {Code} ", code);

        await SendConfirmationEmail(user, code);

        return Result.Success();


    }

    public async Task<Result> SendResetPasswordCodeAsync(ForgetPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return Result.Success();



        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        _logger.LogInformation("Email confirmation code for user: {Code} ", code);

        await SendResetPasswordEmail(user, code);

        return Result.Success();


    }


    public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null || !user.EmailConfirmed)
            return Result.Fail(UserError.invalidCode);

        IdentityResult result;

        try
        {
            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
            result = await _userManager.ResetPasswordAsync(user, code, request.NewPassword);
        }
        catch (FormatException)
        {
            result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());
        }

        if (result.Succeeded)
            return Result.Success();


        var error = result.Errors.First();

        return Result.Fail(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }





    private async Task SendConfirmationEmail(ApplicationUser user, string code)
    {
        var orign = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

        var emailbody = EmailBodyBuilder.GenerateEmailBody("ConfirmEmail", new Dictionary<string, string>
             {
                 { "{{name}}" , user.FirstName },

                 { "{{action_url}}" ,  $"{ orign}/auth/emailconfirmation?userid={user.Id}&code={code}" }
             });

        BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "Confirm your email", emailbody));
        await Task.CompletedTask;

    }
    private async Task SendResetPasswordEmail(ApplicationUser user, string code)
    {
        var orign = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

        var emailbody = EmailBodyBuilder.GenerateEmailBody("ConfirmEmail", new Dictionary<string, string>
             {
                 { "{{name}}" , user.FirstName },

                 { "{{action_url}}" ,  $"{ orign}/auth/changepassword?useremail={user.Email}&code={code}" }
             });

        BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "🔐 Change Password ", emailbody));
        await Task.CompletedTask;

    }

    private async Task<(IEnumerable<string> roles, IEnumerable<string> permission)> GetRoleAndPermission(ApplicationUser user
        , CancellationToken cancellationToken)
    {

        var UserRoles = await _userManager.GetRolesAsync(user);

        //var UserPermission = await _context.Roles.
        //             Join(_context.RoleClaims, role => role.Id, claims => claims.RoleId, (role, claims) => new { role, claims })
        //             .Where(x => UserRoles.Contains(x.role.Name!))
        //             .Select(x => x.claims.ClaimValue)
        //            .Distinct()
        //            .ToListAsync(cancellationToken);


        var UserPermission = await (from r in _context.Roles
                                    join p in _context.RoleClaims on r.Id equals p.RoleId
                                    where UserRoles.Contains(r.Name!)
                                    select p.ClaimValue).
                             Distinct().
                             ToListAsync(cancellationToken);


        return (UserRoles, UserPermission);
    }
}
