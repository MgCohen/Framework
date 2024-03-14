using System;
using UnityEngine;

namespace Scaffold.Screens.Core
{
    [Serializable]
    public class StackedScreen
    {
        public StackedScreen(IScreen screen, bool destroyOnClose)
        {
            this.Screen = screen;
            this.ScreenObject = screen.gameObject;
            this.DestroyOnClose = destroyOnClose;
        }

        public StackedScreen(IScreen screen, IScreenContext context, bool destroyOnClose)
        {
            this.Screen = screen;
            this.ScreenObject = screen.gameObject;
            this.Context = context;
            this.DestroyOnClose = destroyOnClose;
        }

        public IScreen Screen { get; private set; }
        public IScreenContext Context { get; private set; }
        public GameObject ScreenObject { get; private set; }
        public bool DestroyOnClose { get; private set; }
        public ScreenConfig Config { get; private set; }

        public void DefineContext(IScreenContext context)
        {
            Context = context;
            if (Screen is IScreenT screenT)
            {
                screenT.SetContext(context);
            }
        }
    }
}
