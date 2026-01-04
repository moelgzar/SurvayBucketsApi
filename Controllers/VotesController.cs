using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Contracts.Vote;
using SurvayBucketsApi.Errors;
using SurvayBucketsApi.Extensions;

namespace SurvayBucketsApi.Controllers;
[Route("api/polls/{pollId}/vote")]
[ApiController]
//[Authorize]
public class VotesController(IQuestionServices questionServices , IVoteServices voteServices) : ControllerBase
{
    private readonly IQuestionServices _questionServices = questionServices;
    private readonly IVoteServices _voteServices = voteServices;

    [HttpGet("")]
    [OutputCache(Duration =60)]
    public async Task<IActionResult> Start([FromRoute] int pollid, CancellationToken cancellationToken)
    {
       
        var userid = "56026f6f-421c-496f-9a21-fb0df73b6123";

        var result = await _questionServices.GetAvilabelAsync(pollid, userid, cancellationToken);

        
        return result.IsSuccess ? Ok(result.Value) : 
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
