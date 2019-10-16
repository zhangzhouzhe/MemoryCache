using System;
using System.Collections.Generic;
using System.Text;

namespace MyCache
{
    public interface ICacheEntry
    {
        string Key { get; }
        int Version { get; }
        ExpirationMode ExpirationMode { get; }

        DateTime CreateUtc { get;}

        TimeSpan ExpirationTimeout { get; }

        DateTime LastAccessedUtc { get; }
    }

    public enum ExpirationMode
    {
        Default = 0,
        None = 1,
        Sliding = 2,
        Absolute = 3
    }
}
