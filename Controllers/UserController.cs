using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Abstractions.Const;
using SurvayBucketsApi.Authorization.Filter;
using SurvayBucketsApi.Contracts.User;

namespace SurvayBucketsApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;


    [HttpGet("")]
    [HasPermission(Permissions.GetUser)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _userService.GetAllUser(cancellationToken));
    }
    [HttpGet("{id}")]
    [HasPermission(Permissions.GetUser)]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var result = await _userService.Getbyid(id);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpPost("")]
    [HasPermission(Permissions.AddUser)]
    public async Task<IActionResult> Add([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.CreateUserAsync(request, cancellationToken);

        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value) : result.ToProblem();
    }


    [HttpPut("{id}")]
    [HasPermission(Permissions.UpdateUser)]
    public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateUserAsync(id, request, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id}/toggle-status")]
    [HasPermission(Permissions.UpdateUser)]
    public async Task<IActionResult> Toggle([FromRoute] string id, CancellationToken cancellationToken)
    {
        var result = await _userService.ToggleAsync(id, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id}/unlock")]
    [HasPermission(Permissions.UpdateUser)]
    public async Task<IActionResult> UnLock([FromRoute] string id, CancellationToken cancellationToken)
    {
        var result = await _userService.UnLock(id, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
