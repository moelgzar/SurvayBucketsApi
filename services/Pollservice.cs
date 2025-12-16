
namespace SurvayBucketsApi.services;

public class Pollservice(ApplicationDbContext context) : IPollservice

{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellation = default)
    {
        return await  _context.polls.AsNoTracking().ToListAsync(cancellation);
    }

    public async Task<Poll> AddPollAsync(Poll poll , CancellationToken cancellation = default)
    {       
        await _context.polls.AddAsync(poll , cancellation);
        await _context.SaveChangesAsync(cancellation);
        return poll;
    }

    public async Task<bool> UpdatePollAsync(int id, Poll poll , CancellationToken cancellation)
    {
        var currentpoll = await GetAsync(id , cancellation);
        if (currentpoll is null)
            return false;


        currentpoll.Title = poll.Title;
        currentpoll.Summray = poll.Summray;
        currentpoll.StartsAt = poll.StartsAt;
        currentpoll.EndsAt = poll.EndsAt;

        await _context.SaveChangesAsync(cancellation);

        return true;
    }

    public async Task <Poll?> GetAsync(int id , CancellationToken cancellation = default)
    {
        return  await _context.polls.FindAsync(id, cancellation);

    }

    public async Task <bool> DeletePollAsync(int id , CancellationToken cancellation)
    {
        var poll = await GetAsync(id , cancellation);
        if (poll is null)
            return false;
        _context.Remove(poll);
        await _context.SaveChangesAsync(cancellation);
        return true;
    }

    public async Task<bool> togglePublishStatus(int id, CancellationToken cancellation)
    {
        var currentpoll = await GetAsync(id, cancellation);
        if (currentpoll is null)
            return false;


      currentpoll.IsPublished = !currentpoll.IsPublished;

        await _context.SaveChangesAsync(cancellation);

        return true;
    }
}