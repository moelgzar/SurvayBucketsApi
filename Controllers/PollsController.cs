
using System.Security.Cryptography.X509Certificates;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SurvayBucketsApi.Controllers;


[Route("api/[controller]")]
[ApiController] // responsbile for binding process 
public class PollsController(IPollservice pollservice) : ControllerBase
{
   private readonly IPollservice _pollservice = pollservice;

    [HttpGet("")]
    public IActionResult Get()
    {
        var polls = _pollservice.GetAll();
        var response = polls.Adapt<IEnumerable<PollResponse>>();
        return Ok(response);
    }


    [HttpGet("{id}")]
    public IActionResult Get( [FromRoute] int id )
    {
        var poll = _pollservice.GetPollById(id) ;
        if(poll is null)
        {
            return NotFound();
        }
       
        var response = poll.Adapt<PollResponse>();
        return Ok(response);
    }

    [HttpPost("")]
    public IActionResult Add( [FromBody] CrearePollRequest request )
    {
       
      var newpoll =   _pollservice.AddPoll(request.Adapt<Poll>());

        return CreatedAtAction( nameof(Get), new { id = newpoll.id} , newpoll);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int  id , [FromBody] CrearePollRequest request)
    {
        var updatedPoll = _pollservice.UpdatePoll(id , request.Adapt<Poll>());
        if (!updatedPoll)
            return NotFound();
        
     
        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id) {

        bool isdeleted =  _pollservice.DeletePoll(id);

        if(!isdeleted)
                return NotFound();
        
        return NoContent();

    }




}
