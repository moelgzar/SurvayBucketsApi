
using System.Security.Cryptography.X509Certificates;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SurvayBucketsApi.Contracts.Polls;

namespace SurvayBucketsApi.Controllers;


[Route("api/[controller]")]
[ApiController] // responsbile for binding process 
public class PollsController(IPollservice pollservice) : ControllerBase
{
   private readonly IPollservice _pollservice = pollservice;

    [Authorize]
    [HttpGet("")]
    public async Task <IActionResult> Get(CancellationToken cancellation = default)
    {
        var polls = await _pollservice.GetAllAsync(cancellation);
        var response = polls.Adapt<IEnumerable<PollResponse>>();
        return Ok(response);
    }


    [HttpGet("{id}")]
    public async Task <IActionResult> Get([FromRoute] int id , CancellationToken cancellation = default)
    {
        var poll = await  _pollservice.GetAsync(id , cancellation);
        if (poll is null)
        {
            return NotFound();
        }
        var response = poll.Adapt<PollResponse>();
        return Ok(response);
    }

    [HttpPost("")]
    public async Task <IActionResult> Add([FromBody] PollRequest request , CancellationToken cancellation = default)
    {

        var newpoll = await  _pollservice.AddPollAsync(request.Adapt<Poll>() , cancellation);

        return  CreatedAtAction(nameof(Get), new { id = newpoll.Id } ,  newpoll);
    }

    [HttpPut("{id}")]
    public async Task <IActionResult> Update([FromRoute] int id, [FromBody] PollRequest request , CancellationToken cancellation)
    {
        var updatedPoll = await _pollservice.UpdatePollAsync(id, request.Adapt<Poll>() , cancellation);
        if (!updatedPoll)
            return  NotFound();

        
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task <IActionResult> Delete([FromRoute] int id , CancellationToken cancellation)
    {

        bool isdeleted = await _pollservice.DeletePollAsync(id , cancellation);

        if (!isdeleted)
            return NotFound();

        return NoContent();

    }


    [HttpPut("{id}/tooglePublish")]
    public async Task<IActionResult> TooglePublish([FromRoute] int id,  CancellationToken cancellation)
    {
        var updatedPoll = await _pollservice.togglePublishStatus(id,  cancellation);
        if (!updatedPoll)
            return NotFound();


        return NoContent();
    }

}
