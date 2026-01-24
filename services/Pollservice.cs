
using Hangfire;
using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Errors;

namespace SurvayBucketsApi.services;

public class Pollservice(ApplicationDbContext context , INotificationService notificationService) : IPollservice

{
    private readonly ApplicationDbContext _context = context;
    private readonly INotificationService _notificationService = notificationService;

    public async Task<IEnumerable<PollResponse>> GetAllAsync(CancellationToken cancellation = default)
    {
        return await _context.polls.ProjectToType<PollResponse>().AsNoTracking().ToListAsync(cancellation);
    }

    public async Task<IEnumerable<PollResponse>> GetCurrentAsync(CancellationToken cancellation = default)
    {
        return await _context.polls.
            Where(x=>x.IsPublished && x.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow) 
                 && x.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow))
                .AsNoTracking()
                .ProjectToType<PollResponse>().ToListAsync(cancellation);
    }


    public async Task<IEnumerable<PollResponseV2>> GetCurrentAsyncV2(CancellationToken cancellation = default)
    {
        return await _context.polls.
            Where(x => x.IsPublished && x.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow)
                 && x.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow))
                .AsNoTracking()
                .ProjectToType<PollResponseV2>().ToListAsync(cancellation);
    }
    public async Task<Result<PollResponse>> AddPollAsync(PollRequest request, CancellationToken cancellation = default)
    {
        var IsExisiting = await _context.polls.AnyAsync(p => p.Title == request.Title, cancellationToken: cancellation);

        if (IsExisiting)
            return Result.Fail<PollResponse>(PollError.PollDuplcated);

        var result =  await _context.polls.AddAsync(request.Adapt<Poll>(), cancellation);
        await _context.SaveChangesAsync(cancellation);

        if (result is null)
            return Result.Fail<PollResponse>(PollError.PollNotCreated);
        return Result.Success(result.Adapt<PollResponse>());


    }

    public async Task<Result> UpdatePollAsync(int id, PollRequest request, CancellationToken cancellation)
    {

        var IsExisiting = await _context.polls.AnyAsync(p => p.Title == request.Title && p.Id != id, cancellationToken: cancellation);

        if (IsExisiting)
            return Result.Fail<PollResponse>(PollError.PollDuplcated);


        var currentpoll = await _context.polls.FindAsync(id, cancellation);
        if (currentpoll is null)
            return Result.Fail(PollError.PollNotFound) ;


        currentpoll.Title = request.Title;
        currentpoll.Summray = request.Summray;
        currentpoll.StartsAt = request.StartsAt;
        currentpoll.EndsAt = request.EndsAt;
        currentpoll.IsPublished = request.IsPublished;

        await _context.SaveChangesAsync(cancellation);

        return Result.Success();
    }

    public async Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellation = default)
    {
        var result = await _context.polls.FindAsync(id, cancellation);

        if (result is null)
            return Result.Fail<PollResponse>(PollError.PollNotFound);

        return Result.Success(result.Adapt<PollResponse>());


    }

    public async Task<Result> DeletePollAsync(int id, CancellationToken cancellation)
    {
        var poll = await _context.polls.FindAsync(id, cancellation);
        if (poll is null)
            return Result.Fail(PollError.PollNotFound);
        _context.Remove(poll);
        await _context.SaveChangesAsync(cancellation);
        return Result.Success();
    }

    public async Task<Result> togglePublishStatus(int id, CancellationToken cancellation)
    {
        var poll = await _context.polls.FirstOrDefaultAsync(x=>x.Id == id);
        if (poll is null)
            return Result.Fail(PollError.PollNotFound);

        await _context.SaveChangesAsync(cancellation);

        if (poll.IsPublished && poll.StartsAt == DateOnly.FromDateTime(DateTime.UtcNow))

            BackgroundJob.Enqueue( () => _notificationService.SendNewPollsNotifications(poll.Id));
        return Result.Success();
    }

    
}