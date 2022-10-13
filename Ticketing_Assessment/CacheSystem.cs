using System;
using System.Collections.Generic;

namespace Ticketing_Assessment
{
    public class CacheSystem<TKey>
    {
        private readonly Dictionary<TKey, CachedCityDistance> _cache= new ();
        private readonly TimeSpan _maxCachingTime;

        public CacheSystem() : this(TimeSpan.MaxValue)
        {
        }

        public CacheSystem(TimeSpan maxCachingTime)
        {
            _maxCachingTime = maxCachingTime;
        }

        public int GetDistance(TKey key, Func<int> fetchDistance)
        {
            CachedCityDistance cachedItem;

            if (_cache.TryGetValue(key, out cachedItem) && DateTime.Now - cachedItem.CacheTime <= _maxCachingTime)
            {
                return cachedItem.CityDistance;
            }

            var cityDistance = fetchDistance();

            if (cityDistance < 0) return cityDistance;

            _cache[key] = new CachedCityDistance(cityDistance);

            return cityDistance;
        }
    }
}