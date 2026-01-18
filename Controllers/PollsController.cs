
using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Abstractions.Const;
using SurvayBucketsApi.Authorization.Filter;
using SurvayBucketsApi.Errors;

namespace SurvayBucketsApi.Controllers;


[Route("api/[controller]")]
[ApiController] // responsbile for binding process

public class PollsController(IPollservice pollservice) : ControllerBase
{
   private readonly IPollservice _pollservice = pollservice;

    [HttpGet("")]
    [HasPermission(Permissions.GetPoll)]
    public async Task <IActionResult> Get(CancellationToken cancellation = default)
    {
        return Ok(await _pollservice.GetAllAsync(cancellation));
    }


    [HttpGet("current")]
    [Authorize(Roles = DefaultRole.Member)]
    [HasPermission(Permissions.GetPoll)]

    public async Task<IActionResult> GetCurrent(CancellationToken cancellation = default)
    {
        return Ok(await _pollservice.GetCurrentAsync(cancellation));
    }



    [HttpGet("{id}")]
    [HasPermission(Permissions.GetPoll)]
    public async Task <IActionResult> Get([FromRoute] int id , CancellationToken cancellation = default)
    {
        var poll = await  _pollservice.GetAsync(id , cancellation);
       
        return poll.IsSuccess ? Ok(poll.Value) : Problem(
            statusCode: 404,
            title: "Poll Not Found",
            detail: "The poll with the specified ID was not found."
        );
    }

    [HttpPost("")]
    [HasPermission(Permissions.AddPoll)]

    public async Task<IActionResult> Add([FromBody] PollRequest request, CancellationToken cancellation = default)
    {
      
        var result = await _pollservice.AddPollAsync(request, cancellation);

        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value) 
            : result.ToProblem();
    }
    [HttpPut("{id}")]
    [HasPermission(Permissions.UpdatePoll)]
    public async Task <IActionResult> Update([FromRoute] int id, [FromBody] PollRequest request , CancellationToken cancellation)
    {
        var Result = await _pollservice.UpdatePollAsync(id, request , cancellation);
        
        return Result.IsSuccess ? NoContent() : NotFound(PollError.PollNotFound);
    }
    [HttpDelete("{id}")]
    [HasPermission(Permissions.DeletePoll)]
    public async Task <IActionResult> Delete([FromRoute] int id , CancellationToken cancellation)
    {

        var Result = await _pollservice.DeletePollAsync(id , cancellation);

      
        return Result.IsSuccess ? NoContent() : NotFound(PollError.PollNotFound);

    }


    [HttpPut("{id}/tooglePublish")]
    [HasPermission(Permissions.UpdatePoll)]
    public async Task<IActionResult> TooglePublish([FromRoute] int id,  CancellationToken cancellation)
    {
        var result = await _pollservice.togglePublishStatus(id,  cancellation);
       
        return result.IsSuccess ? NoContent() : NotFound(PollError.PollNotFound);
    }

}
