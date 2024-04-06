using System.Collections;

namespace Scaffold.Screens.Core
{
    public class ScreenNavigation
    {
        public ScreenNavigation(ScreenSettings settings)
        {
            this.settings = settings;
        }

        private ScreenSettings settings;
        private ScreenTransitionController controller;

        public void DoTransition(TransitionType type, StackedScreen from, StackedScreen to)
        {
            //TODO: Set default behaviours
            //set screen layer - probably set layer on the get? or when transtionCO starts?
            //remember to do overlay
            //clean up screen open/close callbacks

            //need to support In&Out and also Smooth Transition(in and out at the same time)
            ScreenTransition transition = GetTransition(from, to);
            controller.QueueSequence(TransitionCO(type, transition, from, to));
        }

        private IEnumerator TransitionCO(TransitionType type, ScreenTransition transition, StackedScreen from, StackedScreen to)
        {
            //signal before transition
            yield return transition.DoTransition(type, from, to);
            //signal after transtion
        }

        private ScreenTransition GetTransition(StackedScreen from, StackedScreen to)
        {
            //TODO: check if any of the screens have a custom transition for this pair(or global)
            return settings.DefaultTransition;
        }
    }
}