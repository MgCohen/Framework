using Zenject;

namespace Scaffold.Screens.Injected
{
    public class ScreenServiceSignalHandler
    {
        public ScreenServiceSignalHandler(IScreenService service, SignalBus signals)
        {
            this.signals = signals;
            service.OnScreenChanged += OnScreenChanged;
        }

        private SignalBus signals;

        private void OnScreenChanged(IScreen fromScreen, IScreen toScreen)
        {
            signals.Fire(new ScreenChangedSignal(fromScreen, toScreen));
        }
    }
}
