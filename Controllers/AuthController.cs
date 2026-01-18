using Microsoft.Extensions.Options;
using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Authorization;
using SurvayBucketsApi.Contracts.Authorization;
using SurvayBucketsApi.Contracts.Register;
using SurvayBucketsApi.Errors;

namespace SurvayBucketsApi.Controllers;
[Route("[controller]")]
[ApiController]

public class AuthController(IAuthService authService, IOptions<JwtOptions> Jwtoptions, ILogger<AuthController> logger) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly ILogger<AuthController> _logger = logger;
    public JwtOptions _jwtOptions = Jwtoptions.Value;

    [HttpPost("")]
    public async Task<IActionResult> LoginAsync(LoginRequestDto loginRequest, CancellationToken cancellation)
    {

        _logger.LogWarning("Login attempt for hellow ");
        var autoresult = await _authService.GetTokenAsync(loginRequest.Email, loginRequest.Password, cancellation);


        return autoresult.IsSuccess ? Ok(autoresult.Value) : autoresult.ToProblem();

    }

    [HttpPut("RefreshToken")]
    public async Task<IActionResult> GenerateRefreshTokenAsync(RefreshTokenRequest Request, CancellationToken cancellation)
    {

        var autoresult = await _authService.GetRefreshTokenAsync(Request.Token, cancellation);


        //return autoresult is null ? BadRequest("Reffresh token  not correct ") : Ok(autoresult);
        return autoresult.IsSuccess ? Ok(autoresult.Value) : NotFound(UserError.UserRefreshTokenNotFound);

    }
    [HttpPut("RevokeToken")]
    public async Task<IActionResult> RevokeRefreshTokenAsync(RevokeTokenRequest Request, CancellationToken cancellation)
    {

        var autoresult = await _authService.RevokeRefreshTokenAsync(Request.Token, Request.RefresheToken, cancellation);


        return autoresult ? Ok() : BadRequest("email or password not correct ");

    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestShape Request, CancellationToken cancellation)
    {

        var result = await _authService.RegisterAsync(Request, cancellation);


        return result.IsSuccess ? Ok() : result.ToProblem();

    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest Request)
    {

        var result = await _authService.ConfirmEmailAsync(Request);


        return result.IsSuccess ? Ok() : result.ToProblem();

    }

    [HttpPost("resend-confirm-email")]
    public async Task<IActionResult> ResendConfirmEmail([FromBody] ResendConfirmEmail Request)
    {

        var result = await _authService.ResendConfirmEmailAsync(Request);


        return result.IsSuccess ? Ok() : result.ToProblem();

    }
    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest Request)
    {

        var result = await _authService.SendResetPasswordCodeAsync(Request);


        return result.IsSuccess ? Ok() : result.ToProblem();

    }
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {

        var result = await _authService.ResetPasswordAsync(request);


        return result.IsSuccess ? Ok() : result.ToProblem();

    }
}