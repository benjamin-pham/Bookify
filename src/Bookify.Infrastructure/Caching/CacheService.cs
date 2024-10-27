using Bookify.Application.Abstractions.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System.Buffers;
using System.Text.Json;

namespace Bookify.Infrastructure.Caching;
internal class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<T?>? GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        byte[]? bytes = await _distributedCache.GetAsync(key, cancellationToken);

        return bytes is null ? default : Deserialize<T>(bytes);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        return _distributedCache.RemoveAsync(key, cancellationToken);
    }

    public Task SetAsync<T>(
        string key,
        T value,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
    {
        byte[] bytes = Serialize(value);

        return _distributedCache.SetAsync(key, bytes, CacheOptions.Create(expiration), cancellationToken);
    }

    private static T Deserialize<T>(byte[] bytes)
    {
        return JsonSerializer.Deserialize<T>(bytes)!;
    }

    private static byte[] Serialize<T>(T value)
    {
        ArrayBufferWriter<byte> buffer = new ArrayBufferWriter<byte>();

        using Utf8JsonWriter writer = new Utf8JsonWriter(buffer);

        JsonSerializer.Serialize(writer, value);

        return buffer.WrittenSpan.ToArray();
    }
}
