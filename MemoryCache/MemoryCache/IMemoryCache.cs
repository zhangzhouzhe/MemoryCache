using System;

namespace MemoryCache
{
    public interface IMemoryCache
    {
        bool TryGetValue(object key, out object value);
        ICacheEntry CreateEntry(object key);
        void Remove(object key);
    }
}
