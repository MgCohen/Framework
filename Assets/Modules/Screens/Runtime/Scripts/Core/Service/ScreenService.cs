using System;
using System.Linq;
using System.Collections;
using UnityEngine;

namespace Scaffold.Screens.Core
{
    public class ScreenService : IScreenService
    {
        public ScreenService(IScreenProvider provider, ScreenTransitionController animations, ScreenStack stack, ScreenCache cache, ScreenSettings settings)
        {
            this.provider = provider;
            this.screenAnimations = animations;
            this.stack = stack;
            this.cache = cache;
            this.settings = settings;
        }

        public IScreen CurrentScreen => stack.CurrentScreen;
        public StackedScreen CurrentStackedScreen => stack.CurrentStackedScreen;

        private IScreenProvider provider;
        private ScreenTransitionController screenAnimations;
        private ScreenStack stack;
        private ScreenCache cache;
        private ScreenSettings settings;

        private OverlayService overlays;

        public event Action<IScreen, IScreen> OnScreenChanged = delegate { };

        #region Navigation

        public T Open<T>(bool closeCurrent = false) where T : IScreen
        {
            return Open<T>(null, closeCurrent);
        }

        public IScreen Open(Type screenType, bool closeCurrent = false)
        {
            StackedScreen stack = provider.GetScreen(screenType);
            return OpenStackedScreen(stack, closeCurrent);
        }

        public T Open<T>(IScreenContext context, bool closeCurrent = false) where T : IScreen
        {
            StackedScreen stack = provider.GetScreen<T>();
            if (context != null)
            {
                stack.DefineContext(context);
            }
            return (T)OpenStackedScreen(stack, closeCurrent);
        }

        private IScreen OpenStackedScreen(StackedScreen screen, bool closeCurrent = false)
        {
            screenAnimations.QueueSequence(OpenSequence(screen, closeCurrent));
            return screen.Screen;
        }

        public void Close<T>() where T : IScreen
        {
            StackedScreen stackedScreen = stack.Get<T>();
            CloseStackedScreen(stackedScreen);
        }

        public void Close(IScreen screen)
        {
            StackedScreen stackedScreen = stack.Get(screen);
            CloseStackedScreen(stackedScreen);
        }

        private void CloseStackedScreen(StackedScreen screen)
        {
            screenAnimations.QueueSequence(CloseSequence(screen));
        }

        public void CloseCurrentScreen()
        {
            if (stack.CurrentScreen != null)
            {
                Close(stack.CurrentScreen);
            }
        }

        public void CloseAll()
        {
            for (int i = stack.Count - 2; i >= 0; i--)
            {
                GameObject.Destroy(stack.Stack[i].ScreenObject);
                stack.RemoveFromStack(stack.Stack[i]);
            }
            Close(CurrentScreen);
            stack.ClearStack();
        }

        private void FocusCurrentScreen()
        {
            if (stack.Count > 0)
            {
                StackedScreen stackedScreen = stack.Stack[^1];
                var context = stackedScreen.Context;
                var screen = stackedScreen.Screen;
                if (screen is IScreenT tscreen)
                {
                    tscreen.SetContext(context);
                }
                screen.Focus();
            }
        }

        private void HideCurrentScreen()
        {
            if (CurrentScreen != null)
            {
                CurrentScreen.Hide();
            }
        }

        #endregion

        #region Sequences
        private IEnumerator OpenSequence(StackedScreen stacked, bool closeCurrent = false, bool notify = true)
        {
            var oldScreen = CurrentScreen;

            if (CurrentScreen == stacked.Screen)
            {
                //do nothing, its the same screen with a diferent context
            }
            else if (CurrentScreen?.ScreenType is ScreenType.Overlay)
            {
                //do nothing, overlays should manage itself
            }
            else if (closeCurrent && CurrentStackedScreen != null)
            {
                yield return CloseOthersSequence(stacked, false);
            }
            else
            {
                HideCurrentScreen();
            }

            stack.AddToStack(stacked);
            stacked.ScreenObject.SetActive(true);
            stacked.Screen.SetLayer(stack.Count * 2); //multiply by 2 so there is always one empty layer for extras and overlays

            if (notify)
            {
                OnScreenChanged?.Invoke(oldScreen, stacked.Screen);
            }
            yield return stacked.Screen.Open();
            yield return overlays.OpenMissingOverlays(stacked.Config);
        }

        private IEnumerator CloseSequence(StackedScreen stacked, bool notify = true)
        {
            stack.RemoveFromStack(stacked);
            FocusCurrentScreen(); //CurrentScreen is now the previous screen, Unhide/focus it to guarantee there is something behind when closing
            yield return stacked.Screen.Close();

            stacked.ScreenObject.SetActive(false); //just in case the animation didnt force this
            cache.CacheScreen(stacked);

            if (notify)
            {
                OnScreenChanged?.Invoke(stacked.Screen, CurrentScreen);
            }
        }

        private IEnumerator CloseOthersSequence(StackedScreen stacked, bool notify = true)
        {
            var screens = stack.GetAllScreens(st => st.Screen != CurrentScreen && st.Screen != stacked.Screen).Distinct();
            foreach (var screen in screens)
            {
                GameObject.Destroy(screen.gameObject);
            }
            stack.ClearStack();
            yield return CloseSequence(CurrentStackedScreen, notify);
        }
        #endregion
    }
}
