using Zenject;

namespace Scaffold.Screens.Injected
{
    public abstract class InjectedScreen<T> : Screen where T : IScreenContext
    {
        [Inject] protected T Context;

        protected override void Setup()
        {
            base.Setup();
            Context.ContextUpdated -= OnContextUpdated;
            Context.ContextUpdated += OnContextUpdated;
        }

        protected virtual void OnContextUpdated() { }
    }
}
