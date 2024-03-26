using UnityEngine;

namespace Scaffold.Screens.Core
{
    public class ScreenCacheOptions
    {
        public bool LimitCacheSize => limitCacheSize;
        [SerializeField] private bool limitCacheSize;

        public int MaxCachedScreens => maxCachedScreens;
        [SerializeField] private int maxCachedScreens;

        public bool LimitCacheLifeTime => limitCacheLifetime;
        [SerializeField] private bool limitCacheLifetime;

        public int CachedScreenLifetimeInMinutes => cachedScreenLifetimeInMinutes;
        [SerializeField] private int cachedScreenLifetimeInMinutes;
    }
}
