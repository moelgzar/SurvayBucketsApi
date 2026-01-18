using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Contracts.User;
using SurvayBucketsApi.Extensions;

namespace SurvayBucketsApi.Controllers;
[Route("me")]
[ApiController]
[Authorize]
public class AccountController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("")]
    public async  Task<IActionResult> Info()
    {

        var user  = await _userService.GetProfile(User.GetUserId());
        return Ok(user.Value);

    }

    [HttpPut("info")]
    public async Task<IActionResult> Update( UpdateProfileRequest request)
    {

    await _userService.UpdateProfile(User.GetUserId() , request);

        return NoContent();
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> Change(ChangePasswordRequest request)
    {

        var result = await _userService.ChangePassword(User.GetUserId() , request);

        return result.IsSuccess ? NoContent() : result.ToProblem();

       
    }



}
