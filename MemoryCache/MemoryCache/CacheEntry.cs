using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryCache
{
    class CacheEntry
    {
    }
    public interface ICacheEntry
    {
        object Key { get; set; }
        object Value { get; set; }

        DateTimeOffset? AbsoluteExpiration { get; set; }

        TimeSpan? SlidingExpiration { get; set; }

        CacheItemPriority Priorty { get; set; }

        long? Size { get; set; }
    }

    public enum CacheItemPriority
    {
        Low,
        Normal,
        High,
        NeverRemover
    }
}
