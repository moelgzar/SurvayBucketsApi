
using System.Security.Cryptography.X509Certificates;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Contracts.Polls;
using SurvayBucketsApi.Errors;

namespace SurvayBucketsApi.Controllers;


[Route("api/[controller]")]
[ApiController] // responsbile for binding process
    [Authorize]

public class PollsController(IPollservice pollservice) : ControllerBase
{
   private readonly IPollservice _pollservice = pollservice;

    [HttpGet("")]
    public async Task <IActionResult> Get(CancellationToken cancellation = default)
    {
        return Ok(await _pollservice.GetAllAsync(cancellation));
    }


    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent(CancellationToken cancellation = default)
    {
        return Ok(await _pollservice.GetCurrentAsync(cancellation));
    }



    [HttpGet("{id}")]
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
    public async Task<IActionResult> Add([FromBody] PollRequest request, CancellationToken cancellation = default)
    {
      
        var result = await _pollservice.AddPollAsync(request, cancellation);

        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value) 
            : result.ToProblem();
    }
    [HttpPut("{id}")]
    public async Task <IActionResult> Update([FromRoute] int id, [FromBody] PollRequest request , CancellationToken cancellation)
    {
        var Result = await _pollservice.UpdatePollAsync(id, request , cancellation);
        
        return Result.IsSuccess ? NoContent() : NotFound(PollError.PollNotFound);
    }
    [HttpDelete("{id}")]
    public async Task <IActionResult> Delete([FromRoute] int id , CancellationToken cancellation)
    {

        var Result = await _pollservice.DeletePollAsync(id , cancellation);

      
        return Result.IsSuccess ? NoContent() : NotFound(PollError.PollNotFound);

    }


    [HttpPut("{id}/tooglePublish")]
    public async Task<IActionResult> TooglePublish([FromRoute] int id,  CancellationToken cancellation)
    {
        var result = await _pollservice.togglePublishStatus(id,  cancellation);
       
        return result.IsSuccess ? NoContent() : NotFound(PollError.PollNotFound);
    }

}
