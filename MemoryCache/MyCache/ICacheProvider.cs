using System;

namespace MyCache
{
    public interface ICacheProvider
    {
        void Set<T>(string key)
    }
}
