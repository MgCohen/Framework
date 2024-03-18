using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Scaffold.Types;
using System.Linq;

namespace Scaffold.Screens.Core
{

    [CreateAssetMenu(menuName = "Game/Screens/Dictionary")]
    public class ScreenSettings : ScriptableObject
    {
        public ScreenTransition Transition => transition;

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
            [SerializeField] private bool limitCacheSize;
            [SerializeField] private int maxCachedScreens;

            [SerializeField] private bool limitCacheLifetime;
            [SerializeField] private int cachedScreenLifetimeInMinutes;
        }
    }
}
