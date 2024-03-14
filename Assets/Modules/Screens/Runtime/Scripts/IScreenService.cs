using System;
using System.Collections.Generic;

namespace Scaffold.Screens
{
    public interface IScreenService
    {
        IScreen CurrentScreen { get; }
        public event Action<IScreen, IScreen> OnScreenChanged;


        void Close(IScreen screen);
        void Close<T>() where T : IScreen;
        void CloseAll();
        void CloseCurrentScreen();
        T Open<T>(bool closeCurrent = false) where T : IScreen;
        T Open<T>(IScreenContext context, bool closeCurrent = false) where T : IScreen;
        IScreen Open(Type screenType, bool closeCurrent = false);
    }
}