
using SurvayBucketsApi.Abstractions;

namespace SurvayBucketsApi.Controllers;
[Route("api/polls/{pollid}/[controller]")]
[ApiController]
[Authorize]
public class ResultsController(IResultService resultService) : ControllerBase
{
    private readonly IResultService _resultService = resultService;

    [HttpGet("raw-data")]
    public async Task<IActionResult> GetPollVotesAsync([FromRoute] int pollid , CancellationToken cancellationToken)
    {
     
        var result =  await _resultService.ResultsAsync(pollid, cancellationToken);


        return result.IsSuccess ? Ok(result.Value)
            : result.ToProblem();

    }
    [HttpGet("votes-per-day")]
    public  async Task<IActionResult> GetVotesPerDayAsync([FromRoute] int pollid , CancellationToken cancellationToken)
    {
        var result = await _resultService.GetVotesPerDayAsync(pollid, cancellationToken);
        return result.IsSuccess ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpGet("votes-per-question")]
    public async Task<IActionResult> GetVotesPerQustionAsync([FromRoute] int pollid, CancellationToken cancellationToken)
    {
        var result = await _resultService.GetVotesPerQuestionAsync(pollid, cancellationToken);
        return result.IsSuccess ? Ok(result.Value)
            : result.ToProblem();
    }
}
