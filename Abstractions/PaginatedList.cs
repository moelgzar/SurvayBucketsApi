namespace SurvayBucketsApi.Abstractions;

public class PaginatedList<T>(List<T> items, int pageNumber, int count, int pagesize)
{
    public List<T> Items { get; private set; } = items;
    public int PageNumber { get; set; } = pageNumber;
    public int TotalPages => (int)Math.Ceiling(count / (double)pagesize);

    public bool HasNext => PageNumber < TotalPages;
    public bool HasPrevious => PageNumber > 1;

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pagenumber, int pagesize, CancellationToken cancellationToken)
    {
        var count = await source.CountAsync(cancellationToken);
        var items = await source.Skip((pagenumber - 1) * pagesize).Take(pagesize).ToListAsync(cancellationToken);

        return new PaginatedList<T>(items, pagenumber, count, pagesize);
    }

}
