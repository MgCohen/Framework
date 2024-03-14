namespace Scaffold.Screens.Injected
{
    public class ScreenChangedSignal
    {
        public ScreenChangedSignal(IScreen from, IScreen to)
        {
            From = from;
            To = to;
        }

        public IScreen From { get; private set; }
        public IScreen To { get; private set; }
    }
}