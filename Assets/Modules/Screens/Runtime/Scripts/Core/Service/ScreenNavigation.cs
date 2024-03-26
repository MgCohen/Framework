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

        public void DoTransition(StackedScreen from, StackedScreen to)
        {
            //TODO: split transitions based on situation
            //Closing -> OUT current, nothing on previous  (o)current -> (x)previous
            //Openning -> nothing on current, IN on new    (x)current -> (i)new
            //Transition -> out current, in new            (o)current -> (i)new


            ScreenTransition transition = GetTransition(from, to);
            controller.QueueSequence(TransitionCO(transition, from, to));
        }

        private IEnumerator TransitionCO(ScreenTransition transition, StackedScreen from, StackedScreen to)
        {
            //call starting events
            yield return transition.DoTransition(from, to);
            //call finishing events
        }

        private ScreenTransition GetTransition(StackedScreen from, StackedScreen to)
        {
            //TODO: check if any of the screens have a custom transition for this pair(or global)
            return settings.DefaultTransition;
        }
    }
}