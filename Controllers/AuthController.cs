using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Authorization;
using SurvayBucketsApi.Contracts.Authorization;
using SurvayBucketsApi.Errors;

namespace SurvayBucketsApi.Controllers;
[Route("[controller]")]
[ApiController]

public class AuthController(IAuthService authService, IOptions<JwtOptions> Jwtoptions) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    public JwtOptions _jwtOptions = Jwtoptions.Value;

    [HttpPost("")]
    public async Task<IActionResult> LoginAsync(LoginRequestDto loginRequest, CancellationToken cancellation)
    {

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

    //[HttpGet]

    //public IActionResult Test() {


    //    var config = new
    
    //    {
    //        mykey = _jwtOptions.Key,

    //        Audiance = _jwtOptions.Audiance,

    //        Issuer = _jwtOptions.Issuer,

    //    };

    //    return Ok(config);

    //}

}