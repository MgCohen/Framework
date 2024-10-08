﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scaffold.Screens.Core
{
    public class ScreenProvider : IScreenProvider
    {
        public ScreenProvider(ScreenStack stack, ScreenCache cache, IScreenFactory factory)
        {
            this.stack = stack;
            this.cache = cache;
            this.factory = factory;
            this.screenHolder = new GameObject("Screen Holder").transform;
            GetScreensInScene();
        }

        private ScreenStack stack;
        private ScreenCache cache;
        private IScreenFactory factory;
        private Transform screenHolder;
        private Dictionary<Type, IScreen> sceneScreens = new Dictionary<Type, IScreen>();

        private void GetScreensInScene()
        {
            var screens = GameObject.FindObjectsByType<Screen>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var screen in screens)
            {
                sceneScreens[screen.GetType()] = screen;
                screen.gameObject.SetActive(false);
            }
        }

        public StackedScreen GetScreen<T>() where T : IScreen
        {
            return GetScreen(typeof(T));
        }

        public StackedScreen GetScreen(Type screenType)
        {
            if(TryGetCachedScreen(screenType, out IScreen cached))
            {
                return new StackedScreen(cached, false);
            }

            if (TryGetStackedScreen(screenType, out StackedScreen stacked))
            {
                return new StackedScreen(stacked.Screen, stacked.DestroyOnClose);
            }

            if (TryGetSceneScreen(screenType, out IScreen screen))
            {
                return new StackedScreen(screen, false);
            }

            if (TryGetAssetScreen(screenType, out screen))
            {
                return new StackedScreen(screen, true);
            }

            return null;
        }

        private bool TryGetCachedScreen(Type screenType, out IScreen screen)
        {
            return cache.TryRecoverScreen(screenType, out screen);
        }

        private bool TryGetStackedScreen(Type screenType, out StackedScreen stacked)
        {
            stacked = stack.Get(screenType);
            return stacked != null;
        }

        private bool TryGetSceneScreen(Type screenType, out IScreen screen)
        {
            if (screenType != null && sceneScreens.TryGetValue(screenType, out var screenInstance))
            {
                screen = screenInstance;
                return true;
            }
            screen = null;
            return false;
        }

        private bool TryGetAssetScreen(Type screenType, out IScreen screen)
        {
            screen = factory.Create(screenType, screenHolder);
            if (screen != null)
            {
                screen.gameObject.SetActive(false);
                return true;
            }
            else
            {
                screen = null;
                return false;
            }
        }
    }
}
