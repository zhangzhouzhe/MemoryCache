using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryCache
{
    public class CacheEntry
    {
        private bool _added = false;
        private bool _isExpired;
        internal DateTimeOffset? _absoluteExpiration;
        internal TimeSpan? _absoluteExpirationRelativeToNow;
        internal TimeSpan? _slidingExpiration;
        private long? _size;
        internal readonly object _lock = new object();
        private IDisposable _scope;

        private IList<IDisposable> _expirationTokenRegistrations;

        public CacheItemPriority Priority { get; set; } = CacheItemPriority.Normal;
        public DateTimeOffset? AbsoluteExpriration
        {
            get
            {
                return _absoluteExpiration;
            }
            set
            {
                _absoluteExpiration = value.Value;
            }
        }
        public object Key { get; private set; }
        public object Value { get; set; }
        internal DateTimeOffset LastAccessed { get; set; }
        internal bool CheckExpired(DateTimeOffset now)
        {
            return _isExpired || ch
        }
        private bool CheckForExpiredTime(DateTimeOffset now)
        {
            if (_absoluteExpiration.HasValue &&
                _absoluteExpiration.Value <= now)
            {
                return true;
            }
            if (_slidingExpiration.HasValue &&
                (now - LastAccessed) >= _slidingExpiration)
            {
                return true;
            }
            return false;
        }
        internal void SetExpired(EvictionsReason reason)
        {
            _isExpired = true;
            DetachTokens();

        }
        private void DetachTokens()
        {
            lock (_lock)
            {
                var registrations = _expirationTokenRegistrations;
                if (registrations != null)
                    _expirationTokenRegistrations = null;
                for (int i = 0; i < registrations.Count; i++)
                {
                    var registration = registrations[i];
                    registration.Dispose();
                }
            }
        }
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
    public enum EvictionsReason
    {
        None,
        Removed,
        Replaced,
        Expired,
        TokenExpired,
        Capacity
    }
    public enum CacheItemPriority
    {
        Low,
        Normal,
        High,
        NeverRemover
    }
}
