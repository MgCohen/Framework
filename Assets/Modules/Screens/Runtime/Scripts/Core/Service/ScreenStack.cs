using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Scaffold.Screens.Core
{
    public class ScreenStack
    {
        public int Count => stack.Count;
        public IScreen CurrentScreen => CurrentStackedScreen?.Screen;
        public StackedScreen CurrentStackedScreen;

        public List<StackedScreen> Stack => stack;
        private List<StackedScreen> stack = new List<StackedScreen>();

        public StackedScreen Get<T>()
        {
            return Get(typeof(T));
        }

        public StackedScreen Get(Type screenType)
        {
            return stack.LastOrDefault(pair => screenType.IsAssignableFrom(pair.Screen.GetType()));
        }

        public StackedScreen Get(IScreen screen)
        {
            return stack.LastOrDefault(stacked => stacked.Screen == screen);
        }

        public List<StackedScreen> GetAllStackedScreens(Func<StackedScreen, bool> filter)
        {
            filter ??= (s) => true;
            return stack.Where(s => filter.Invoke(s)).ToList();
        }

        public List<IScreen> GetAllScreens(Func<StackedScreen, bool> filter)
        {
            return GetAllStackedScreens(filter).Select(s => s.Screen).ToList();
        }

        public void AddToStack(StackedScreen stacked)
        {
            if (stacked != null)
            {
                stack.Add(stacked);
                CurrentStackedScreen = stacked;
            }
        }

        public void RemoveFromStack(StackedScreen stacked)
        {
            if (stacked != null)
            {
                stack.Remove(stacked);
                if (CurrentScreen == stacked.Screen)
                {
                    CurrentStackedScreen = stack.LastOrDefault();
                }
            }
        }

        public void ClearStack()
        {
            stack.Clear();
        }
    }
}
