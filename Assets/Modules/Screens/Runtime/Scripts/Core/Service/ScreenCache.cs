using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Scaffold.Screens.Core
{
    public class ScreenCache
    {
        public ScreenCache(ScreenStack stack, ScreenSettings settings)
        {
            this.stack = stack;
            cacheOptions = settings.CacheOptions;
        }

        private ScreenStack stack;
        private ScreenSettings.ScreenCacheOptions cacheOptions;
        private Dictionary<IScreen, DateTime> cachedScreens = new Dictionary<IScreen, DateTime>();

        public void CacheScreen(StackedScreen stacked)
        {
            cachedScreens[stacked.Screen] = DateTime.Now;
            if (ShouldClearCache())
            {
                ClearCache();
            }
        }

        public bool TryRecoverScreen(Type screenType, out IScreen screen)
        {
            screen = cachedScreens.FirstOrDefault(vp => vp.Key.GetType().IsAssignableFrom(screenType)).Key;
            if (screen != null)
            {
                cachedScreens.Remove(screen);
                return true;
            }
            return false;
        }

        private bool ShouldClearCache()
        {
            if(cachedScreens.Any(vp => vp.Key == null))
            {
                cachedScreens = cachedScreens.Where(vp => vp.Key != null).ToDictionary(vp => vp.Key, vp => vp.Value);
            }

            if (cacheOptions.LimitCacheSize)
            {
                if (cachedScreens.Count > cacheOptions.MaxCachedScreens)
                {
                    return true;
                }
            }

            if (cacheOptions.LimitCacheLifeTime)
            {
                DateTime minDate = cachedScreens.Min(vp => vp.Value);
                if ((DateTime.Now - minDate).TotalMinutes > cacheOptions.CachedScreenLifetimeInMinutes)
                {
                    return true;
                }
            }

            return false;
        }


        private void ClearCache()
        {
            List<IScreen> screensToRelease = new List<IScreen>();
            var orderedCache = cachedScreens.OrderBy(vp => vp.Value);
            foreach (var screenPair in orderedCache)
            {
                if (cacheOptions.LimitCacheLifeTime)
                {
                    if ((DateTime.Now - screenPair.Value).TotalMinutes > cacheOptions.CachedScreenLifetimeInMinutes)
                    {
                        screensToRelease.Add(screenPair.Key);
                        continue;
                    }

                    //if you hit here, means all other screens are still valid
                    //if you are not limiting cache size, then you can stop iterating
                    if (!cacheOptions.LimitCacheSize)
                    {
                        break;
                    }
                }

                if (cacheOptions.LimitCacheSize)
                {
                    int excessScreenCount = cacheOptions.MaxCachedScreens - screensToRelease.Count;
                    if (excessScreenCount > 0)
                    {
                        screensToRelease.Add(screenPair.Key);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            TryDestroyScreens(screensToRelease);
        }

        private void TryDestroyScreens(List<IScreen> screens)
        {
            foreach (var screen in screens)
            {
                bool hasOtherUses = stack.GetAllStackedScreens(st => st.Screen == screen).Count > 0;
                if (!hasOtherUses)
                {
                    GameObject screenObj = (screen as MonoBehaviour).gameObject;
                    GameObject.Destroy(screenObj);
                    cachedScreens.Remove(screen);
                }
            }

        }
    }
}
