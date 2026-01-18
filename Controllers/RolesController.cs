using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Abstractions.Const;
using SurvayBucketsApi.Authorization.Filter;
using SurvayBucketsApi.Contracts.Roles;

namespace SurvayBucketsApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RolesController(IRoleService roleService) : ControllerBase
{
    private readonly IRoleService _roleService = roleService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll( [FromQuery] bool IncludeDisable  ,  CancellationToken cancellationToken)
    {

        var roles = await _roleService.GetAllRoles(IncludeDisable, cancellationToken); 


        return  Ok(roles); 

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id, CancellationToken cancellationToken)
    {

        var result = await _roleService.GetbyidAsync(id, cancellationToken);



        return  result.IsSuccess  ?  Ok(result.Value) : result.ToProblem();

    }

    [HttpPost("")]
    [HasPermission(Permissions.AddRole)]
    public async Task<IActionResult> Add( [FromBody] RoleRequest request , CancellationToken cancellationToken)
    {

        var result = await _roleService.AddAsync(request , cancellationToken);



        return result.IsSuccess ? CreatedAtAction(nameof(Get) , new { result.Value.Id , result.Value }) : result.ToProblem();

    }
    [HttpPut("{id}")]
    [HasPermission(Permissions.UpdateRole)]
    public async Task<IActionResult> Update( string id ,  [FromBody] RoleRequest request, CancellationToken cancellationToken)
    {

        var result = await _roleService.UpdateAsync(id , request, cancellationToken);



        return result.IsSuccess ? NoContent() : result.ToProblem();

    }
    [HttpPut("{id}/toggle-status")]
    public async Task<IActionResult> Toggle(string id)
    {

        var result = await _roleService.ToggleStatus(id);



        return result.IsSuccess ? Ok() : result.ToProblem();

    }
}
