using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Scaffold.Types;
using System.Linq;

namespace Scaffold.Screens.Core
{

    [CreateAssetMenu(menuName = "Scaffold/Screens/Screen Settings")]

    public class ScreenSettings : ScriptableObject
    {
        public ScreenTransition Transition => transition;
        public ScreenCacheOptions CacheOptions => cacheOptions;

        [SerializeField] private ScreenTransition transition;
        [SerializeField] private List<ScreenConfig> screens = new List<ScreenConfig>();
        [SerializeField] private List<OverlayConfig> overlays = new List<OverlayConfig>();
        [SerializeField] private ScreenCacheOptions cacheOptions = new ScreenCacheOptions();

        public bool TryGetScreenConfig(Type type, out ScreenConfig config)
        {
            config = screens.FirstOrDefault(s => s.Type.IsAssignableFrom(type));
            return config != null;
        }

        public bool TryGetOverlayConfig(Type type, out OverlayConfig config)
        {
            config = overlays.FirstOrDefault(s => s.Type.IsAssignableFrom(type));
            return config != null;
        }

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
}
