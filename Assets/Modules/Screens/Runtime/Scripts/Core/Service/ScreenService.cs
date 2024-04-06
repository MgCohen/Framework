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
        private ScreenNavigation navigation;

        public event Action<IScreen, IScreen> OnScreenChanged = delegate { };

        #region Navigation

        public T Open<T>(bool closeCurrent = false) where T : IScreen
        {
            return Open<T>(null, closeCurrent);
        }

        public IScreen Open(Type screenType, bool closeCurrent = false)
        {
            StackedScreen stack = provider.GetScreen(screenType);
            Open(stack, closeCurrent);
            return stack.Screen;
        }

        public T Open<T>(IScreenContext context, bool closeCurrent = false) where T : IScreen
        {
            StackedScreen stack = provider.GetScreen<T>();
            if (context != null)
            {
                stack.DefineContext(context);
            }
            Open(stack, closeCurrent);
            return (T)stack.Screen;
        }

        private void Open(StackedScreen stacked, bool closeCurrent)
        {
            var previous = CurrentStackedScreen;
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
                Close(CurrentStackedScreen, false);
            }

            TransitionType transition = closeCurrent ? TransitionType.Full : TransitionType.InOnly;
            stack.AddToStack(stacked);
            navigation.DoTransition(transition, previous, stacked);
        }

        public void Close<T>() where T : IScreen
        {
            StackedScreen stackedScreen = stack.Get<T>();
            Close(stackedScreen);
        }

        public void Close(IScreen screen)
        {
            StackedScreen stackedScreen = stack.Get(screen);
            Close(stackedScreen);
        }

        private void Close(StackedScreen stacked, bool notify = true)
        {
            stack.RemoveFromStack(stacked);
            if (notify)
            {
                navigation.DoTransition(TransitionType.OutOnly, stacked, CurrentStackedScreen);
            }
        }

        public void CloseCurrentScreen()
        {
            if (stack.CurrentStackedScreen != null)
            {
                Close(stack.CurrentStackedScreen);
            }
        }

        public void CloseAll()
        {
            //add to cache
            stack.ClearStack();
        }

        #endregion

        //#region Sequences
        //private IEnumerator OpenSequence(StackedScreen stacked, bool closeCurrent = false, bool notify = true)
        //{
        //    var oldScreen = CurrentScreen;

        //    if (CurrentScreen == stacked.Screen)
        //    {
        //        //do nothing, its the same screen with a diferent context
        //    }
        //    else if (CurrentScreen?.ScreenType is ScreenType.Overlay)
        //    {
        //        //do nothing, overlays should manage itself
        //    }
        //    else if (closeCurrent && CurrentStackedScreen != null)
        //    {
        //        yield return CloseOthersSequence(stacked, false);
        //    }
        //    else
        //    {
        //        HideCurrentScreen();
        //    }

        //    stack.AddToStack(stacked);
        //    stacked.ScreenObject.SetActive(true);
        //    stacked.Screen.SetLayer(stack.Count * 2); //multiply by 2 so there is always one empty layer for extras and overlays

        //    if (notify)
        //    {
        //        OnScreenChanged?.Invoke(oldScreen, stacked.Screen);
        //    }
        //    yield return stacked.Screen.Open();
        //    yield return overlays.OpenMissingOverlays(stacked.Config);
        //}

        //private IEnumerator CloseSequence(StackedScreen stacked, bool notify = true)
        //{
        //    stack.RemoveFromStack(stacked);
        //    FocusCurrentScreen(); //CurrentScreen is now the previous screen, Unhide/focus it to guarantee there is something behind when closing
        //    yield return stacked.Screen.Close();

        //    stacked.ScreenObject.SetActive(false); //just in case the animation didnt force this
        //    cache.CacheScreen(stacked);

        //    if (notify)
        //    {
        //        OnScreenChanged?.Invoke(stacked.Screen, CurrentScreen);
        //    }
        //}

        //private void HideCurrentScreen()
        //{
        //    if (CurrentScreen != null)
        //    {
        //        CurrentScreen.Hide();
        //    }
        //}

        //private void FocusCurrentScreen()
        //{
        //    if (stack.Count > 0)
        //    {
        //        StackedScreen stackedScreen = stack.Stack[^1];
        //        var context = stackedScreen.Context;
        //        var screen = stackedScreen.Screen;
        //        if (screen is IScreenT tscreen)
        //        {
        //            tscreen.SetContext(context);
        //        }
        //        screen.Focus();
        //    }
        //}

        //private IEnumerator CloseOthersSequence(StackedScreen stacked, bool notify = true)
        //{
        //    var screens = stack.GetAllScreens(st => st.Screen != CurrentScreen && st.Screen != stacked.Screen).Distinct();
        //    foreach (var screen in screens)
        //    {
        //        GameObject.Destroy(screen.gameObject);
        //    }
        //    stack.ClearStack();
        //    yield return CloseSequence(CurrentStackedScreen, notify);
        //}
        //#endregion
    }
}
