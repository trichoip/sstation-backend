﻿using Microsoft.Extensions.Caching.Distributed;
using ShipperStation.Application.Contracts.Services;
using System.Collections.Concurrent;
using System.Text.Json;

namespace ShipperStation.Infrastructure.Services;
public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);
        if (cachedValue == null)
        {
            return default;
        }

        var value = JsonSerializer.Deserialize<T>(cachedValue);
        return value;
    }

    public async Task<T> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default)
    {
        var cachedValue = await GetAsync<T>(key, cancellationToken);
        if (cachedValue != null)
        {
            return cachedValue;
        }

        cachedValue = await factory();
        if (cachedValue == null)
        {
            throw new NullReferenceException("Cache value is null");
        }

        await SetAsync(key, cachedValue, cancellationToken);

        return cachedValue;
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
    {
        var cachedValue = JsonSerializer.Serialize(value);
        await _distributedCache.SetStringAsync(key, cachedValue, cancellationToken);
        CacheKeys.TryAdd(key, true);
    }

    public async Task SetWithExpirationAsync<T>(string key, T value, TimeSpan duration, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(duration);

        var cachedValue = JsonSerializer.Serialize(value);
        await _distributedCache.SetStringAsync(key, cachedValue, options, cancellationToken);
        CacheKeys.TryAdd(key, true);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _distributedCache.RemoveAsync(key, cancellationToken);
        CacheKeys.TryRemove(key, out _);
    }

    public async Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default)
    {
        var tasks = CacheKeys.Keys
            .Where(key => key.StartsWith(prefix))
            .Select(key => RemoveAsync(key, cancellationToken));

        await Task.WhenAll(tasks);

    }
}
