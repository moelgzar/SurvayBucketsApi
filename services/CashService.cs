using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace SurvayBucketsApi.services;

public class CashService(IDistributedCache distributedCache) : ICashService
{
    private readonly IDistributedCache _distributedCache = distributedCache;

    public async  Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        var cachvalue = await _distributedCache.GetStringAsync(key, cancellationToken);

        return cachvalue is null ? null : JsonSerializer.Deserialize<T>(cachvalue);
    }

    public async Task  RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
      await  _distributedCache.RemoveAsync(key, cancellationToken);
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
    {
       await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(value), cancellationToken);
    }
}
