
namespace SurvayBucketsApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PollsController(IPollservice pollservice) : ControllerBase
{
   private readonly IPollservice _pollservice = pollservice;

    [HttpGet("")]
    public IActionResult Get()
    {
        return Ok(_pollservice.GetAll());
    }


    [HttpGet("{id}")]
    public IActionResult Get(int id )
    {
        var poll = _pollservice.GetPollById(id) ;
        if(poll is null)
        {
            return NotFound();
        }
        return Ok(poll);
    }

    [HttpPost("")]
    public IActionResult Add(Poll request)
    {
       
      var newpoll =   _pollservice.AddPoll(request);

        return CreatedAtAction( nameof(Get), new { id = newpoll.id} , newpoll);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int  id , Poll request)
    {
        var updatedPoll = _pollservice.UpdatePoll(id , request);
        if (!updatedPoll)
            return NotFound();
        
     
        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {

        bool isdeleted =  _pollservice.DeletePoll(id);

        if(!isdeleted)
                return NotFound();
        
        return NoContent();

    }
}
