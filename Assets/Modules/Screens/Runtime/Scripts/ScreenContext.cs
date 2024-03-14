using System;

namespace Scaffold.Screens
{
    public class ScreenContext<T> : IScreenContext where T : IScreen
    {
        public Type ScreenType => typeof(T);

        public event Action ContextUpdated;

        protected void NotifyContentUpdate()
        {
            ContextUpdated?.Invoke();
        }
    }

    public interface IScreenContext
    {
        public Type ScreenType { get; }
        event Action ContextUpdated;
    }
}
