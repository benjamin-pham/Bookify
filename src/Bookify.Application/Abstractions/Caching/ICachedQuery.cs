using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Abstractions.Caching;

public interface ICachedQuery<TRespose> : IQuery<TRespose>, ICachedQuery;

public interface ICachedQuery
{
    string CacheKey { get; }
    TimeSpan? Expiration { get; }
}
