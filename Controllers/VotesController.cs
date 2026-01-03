using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Contracts.Vote;
using SurvayBucketsApi.Errors;
using SurvayBucketsApi.Extensions;

namespace SurvayBucketsApi.Controllers;
[Route("api/polls/{pollId}/vote")]
[ApiController]
[Authorize]
public class VotesController(IQuestionServices questionServices , IVoteServices voteServices) : ControllerBase
{
    private readonly IQuestionServices _questionServices = questionServices;
    private readonly IVoteServices _voteServices = voteServices;

    [HttpGet("")]
    public async Task<IActionResult> Start([FromRoute] int pollid, CancellationToken cancellationToken)
    {
       
        var userid = User.GetUserId();

        var result = await _questionServices.GetAvilabelAsync(pollid, userid, cancellationToken);

        if(result.IsSuccess)
           return Ok(result.Value);
        
        return result.Error.Equals(VoteError.UserAlreadyVoted) 
            ? result.ToProblem() : 
            result.ToProblem()  ;

    }


    [HttpPost("")]
    public async Task<IActionResult> Vote([FromRoute] int pollid, [FromBody] VoteRequest request, CancellationToken cancellationToken)
    {
        var userid = User.GetUserId();

        var result = await _voteServices.AddAsync(pollid, userid, request, cancellationToken);
        


        //return result.IsSuccess 
        //    ? Created()
        //    : result.Error!.Equals(VoteError.UserAlreadyVoted)
        //        ? result.ToProblem(StatusCodes.Status409Conflict)
        //        : result.ToProblem(StatusCodes.Status400BadRequest);

        return result.IsSuccess ? CreatedAtAction(nameof(Start), new { pollId = pollid }, null)
            : result.ToProblem();

    }
}
