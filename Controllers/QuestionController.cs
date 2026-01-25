using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Abstractions.Const;
using SurvayBucketsApi.Authorization.Filter;
using SurvayBucketsApi.Contracts.Common;
using SurvayBucketsApi.Contracts.Question;

namespace SurvayBucketsApi.Controllers;
[Route("api/polls/{pollId}/[controller]")]
[ApiController]
[Authorize]
public class QuestionController(IQuestionServices questionServices) : ControllerBase
{
    private readonly IQuestionServices _questionServices = questionServices;


    [HttpGet("")]
    [HasPermission(Permissions.GetQuestion)]
    public async Task<IActionResult> GetAll([FromRoute] int pollId, [FromQuery] RequestFilter filter, CancellationToken cancellationToken)
    {
        var result = await _questionServices.GetAllAsync(pollId, filter, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }




    [HttpGet("{id}")]
    [HasPermission(Permissions.GetQuestion)]
    public async Task<IActionResult> Get([FromRoute] int pollId, [FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _questionServices.GetAsync(pollId, id, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }



    [HttpPost("")]
    [HasPermission(Permissions.AddQuestion)]

    public async Task<IActionResult> AddAsync([FromRoute] int pollId, [FromBody] QuestionRequest request, CancellationToken cancellationToken)
    {
        var result = await _questionServices.AddAsync(pollId, request, cancellationToken);



        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { pollId, result.Value.id }, result.Value)
            : result.ToProblem();

    }
    [HttpPut("{id}/toggle-status")]
    [HasPermission(Permissions.UpdateQuestion)]
    public async Task<IActionResult> ToggleStatusAsync([FromRoute] int pollId, [FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _questionServices.ToggleStatus(pollId, id, cancellationToken);
        // This is a placeholder for the toggle status functionality.
        // Implement the actual logic in the service layer as needed.
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id}")]
    [HasPermission(Permissions.UpdateQuestion)]

    public async Task<IActionResult> UpdateAsync([FromRoute] int pollId, [FromRoute] int id, [FromBody] QuestionRequest request, CancellationToken cancellationToken)
    {
        var result = await _questionServices.UpdateAsync(pollId, id, request, cancellationToken);

        //return result.IsSuccess ? NoContent() 
        //    : result.Error!.Equals(QuestionError.QuestionDuplcated) ? result.ToProblem(StatusCodes.Status409Conflict)
        //    : result.ToProblem(StatusCodes.Status404NotFound);

        return result.IsSuccess ? NoContent() : result.ToProblem(); //code refactor

    }

}