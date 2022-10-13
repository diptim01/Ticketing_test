using System;

namespace Ticketing_Assessment
{
    public class CachedCityDistance
    {
        public CachedCityDistance(int cityDistance)
        {
            CityDistance = cityDistance;
            CacheTime = DateTime.Now;
        }

        public int CityDistance { get; }
        public DateTime CacheTime { get; }
    }
}