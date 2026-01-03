using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Contracts.Question;
using SurvayBucketsApi.Errors;

namespace SurvayBucketsApi.Controllers;
[Route("api/polls/{pollId}/[controller]")]
[ApiController]
[Authorize]
public class QuestionController(IQuestionServices questionServices) : ControllerBase
{
    private readonly IQuestionServices _questionServices = questionServices;


    [HttpGet("")]
    public async Task <IActionResult> GetAll([FromRoute] int pollId,  [FromBody] QuestionRequest request, CancellationToken cancellationToken)
    {
        var result =  await _questionServices.GetAllAsync(pollId, CancellationToken.None);

        return result.IsSuccess ? Ok( result.Value) : result.ToProblem();
    }




    [HttpGet("{id}")]
    public async Task < IActionResult> Get([FromRoute] int pollId, [FromRoute] int id   , CancellationToken cancellationToken)
    {
        var result = await _questionServices.GetAsync(pollId,id , cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }



    [HttpPost("")]
    public async Task<IActionResult> AddAsync( [FromRoute] int pollId, [FromBody] QuestionRequest request, CancellationToken cancellationToken)
    {
        var result = await _questionServices.AddAsync(pollId, request, cancellationToken);

       

        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { pollId, result.Value.id }, result.Value)
            : result.ToProblem();

    }
    [HttpPut("{id}/toggle-status")]

    public async Task<IActionResult> ToggleStatusAsync([FromRoute] int pollId, [FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _questionServices.ToggleStatus(pollId, id, cancellationToken);
        // This is a placeholder for the toggle status functionality.
        // Implement the actual logic in the service layer as needed.
        return  result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int pollId, [FromRoute] int id, [FromBody] QuestionRequest request, CancellationToken cancellationToken)
    {
        var result = await _questionServices.UpdateAsync(pollId, id, request, cancellationToken);
       
        //return result.IsSuccess ? NoContent() 
        //    : result.Error!.Equals(QuestionError.QuestionDuplcated) ? result.ToProblem(StatusCodes.Status409Conflict)
        //    : result.ToProblem(StatusCodes.Status404NotFound);

        return result.IsSuccess ? NoContent() : result.ToProblem(); //code refactor

    }

}