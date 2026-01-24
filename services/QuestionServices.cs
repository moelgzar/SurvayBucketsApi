using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Hybrid;
using System.Linq.Dynamic.Core;
using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Contracts.Answers;
using SurvayBucketsApi.Contracts.Common;
using SurvayBucketsApi.Contracts.Question;
using SurvayBucketsApi.Entites;
using SurvayBucketsApi.Errors;

namespace SurvayBucketsApi.services;

public class QuestionServices(ApplicationDbContext context , HybridCache hybridCache , ILogger<QuestionServices> logger) : IQuestionServices
{
    private readonly ApplicationDbContext _context = context;
    private readonly HybridCache _hybridCache = hybridCache;
    private readonly ILogger _logger = logger;


    private const string _cachePrefix = "available_questions_";
    public async Task<Result<QuestionResponse>> AddAsync(int pollId, QuestionRequest request, CancellationToken cancellationToken)
    {
        
        var pollIsExist =   _context.polls.Any(p => p.Id == pollId);
        if (!pollIsExist)
            return Result.Fail<QuestionResponse>(PollError.PollNotFound);

        var questionExsists = _context.Questions.Any(q => q.Content == request.Content && q.PollId == pollId);
        
        if (questionExsists)
            return Result.Fail<QuestionResponse>(PollError.PollDuplcated);


        var question = request.Adapt<Question>();
      

        question.PollId = pollId;


        await _context.Questions.AddAsync(question, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        await _hybridCache.RemoveAsync($"{_cachePrefix}-{pollId}", cancellationToken);
        return Result.Success(question.Adapt<QuestionResponse>());


    }

    public async Task<Result<PaginatedList<QuestionResponse>>> GetAllAsync(int pollId, RequestFilter filter ,  CancellationToken cancellationToken)
    {
      
      var   PollIsExist =  await _context.polls.AnyAsync(p => p.Id == pollId, cancellationToken);

        if (!PollIsExist)
            return Result.Fail<PaginatedList<QuestionResponse>>(PollError.PollNotFound);



        var query = _context.Questions
            .Where(q => q.PollId == pollId && (string.IsNullOrEmpty(filter.SearchValue) 
            || q.Content.Contains(filter.SearchValue)))
            .OrderBy($"{filter.SortColumn} {filter.SortDirection}")
            .Include(q => q.Answers)
            .ProjectToType<QuestionResponse>() // Using Mapster's ProjectToType for projection without using Select
            .AsNoTracking();
            

        var question = await PaginatedList<QuestionResponse>.CreateAsync(query,filter.PageNumber , filter.PageSize ,  cancellationToken);

        return Result.Success(question);

    }

    public async Task<Result<QuestionResponse>> GetAsync(int pollId, int id, CancellationToken cancellationToken)
    {
        var question = await _context.Questions
                   .Where(q => q.PollId == pollId && q.Id == id)
                   .Include(q => q.Answers)       
                   .ProjectToType<QuestionResponse>() // Using Mapster's ProjectToType for projection without using Select
                   .AsNoTracking()
                   .SingleOrDefaultAsync(cancellationToken);

        if(question is null )
            return Result.Fail<QuestionResponse>(PollError.PollNotFound);

        return Result.Success(question);
    }

    public async Task<Result<IEnumerable<QuestionResponse>>> GetAvilabelAsync(int pollId, string userid, CancellationToken cancellationToken)
    {
        //var HasVote = await _context.Votes.AnyAsync(v => v.PollId == pollId && v.UserId == userid, cancellationToken);
        //if (HasVote)
        //    return Result.Fail<IEnumerable<QuestionResponse>>(VoteError.UserAlreadyVoted);


        //var PollExsit = await _context.polls.AnyAsync(p => p.Id == pollId && p.IsPublished && p.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow)
        //         && p.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken);

        //if (!PollExsit)
        //    return Result.Fail<IEnumerable<QuestionResponse>>(PollError.PollNotFound);

        var cacheKey = $"{_cachePrefix}-{pollId}";




        var questions = await _hybridCache.GetOrCreateAsync<IEnumerable<QuestionResponse>>(
            cacheKey,
            async entry => await _context.Questions
                   .Where(q => q.PollId == pollId && q.IsActive)
                   .Select(q => new QuestionResponse(
                       q.Id,
                       q.Content,
                       q.Answers
                           .Where(a => a.IsActive)
                           .Select(a => new AnswerResponse(a.Id, a.Content))
                   ))
                   .AsNoTracking()
                   .ToListAsync(cancellationToken)
            

        );

        if (questions is null)
            return Result.Fail<IEnumerable<QuestionResponse>>(PollError.PollNotFound);

        return Result.Success<IEnumerable<QuestionResponse>>(questions);

    }
    public async Task<Result> ToggleStatus([FromRoute] int pollid, [FromRoute] int id ,  CancellationToken cancellation)
    {
        
        var question = _context.Questions.FirstOrDefault(q=>q.PollId == pollid && q.Id == id);
        if( question is null)
            return Result.Fail(PollError.PollNotFound);
        question.IsActive = !question.IsActive;
        await _context.SaveChangesAsync(cancellation);

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(int pollId, int id, QuestionRequest request, CancellationToken cancellationToken)
    {
        
        var IsExsitquestion = await _context.Questions.AnyAsync(x=>x.PollId == pollId && x.Id != id && x.Content == request.Content , cancellationToken);
        if (IsExsitquestion)
            return Result.Fail<QuestionRequest>(QuestionError.QuestionDuplcated);

        var question = await _context.
            Questions.Include(x=>x.Answers)
            .FirstOrDefaultAsync(x=>x.PollId == pollId && x.Id == id ,cancellationToken);

        if (question is null)
            return Result.Fail<QuestionRequest>(QuestionError.QuestionNotFound);
        question.Content = request.Content;
        //update answers

        // current answers in db
        var currentAnswers = question.Answers.Select(x=>x.Content).ToList();
        // new answers from request
        var newAnswers = request.Answers.Except(currentAnswers).ToList();
        
        
        newAnswers.ForEach( async answerContent => 
        {
       
            question.Answers.Add(new Answer
            {
                Content = answerContent
               
            });

        });
        // update IsActive status for existing answers
        question.Answers.ToList().ForEach(answer =>
        {
            answer.IsActive = request.Answers.Contains(answer.Content);
        });

        await _context.SaveChangesAsync(cancellationToken);
        await _hybridCache.RemoveAsync($"{_cachePrefix}-{pollId}", cancellationToken);

        return Result.Success();
    }
}
