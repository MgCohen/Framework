using Scaffold.Screens.Core;
using System.Collections;
using UnityEngine;

namespace Scaffold.Screens
{
    public abstract class ScreenTransition: ScriptableObject
    {
        public abstract IEnumerator DoTransition(StackedScreen from, StackedScreen to);
    }

    public class SimpleTransition : ScreenTransition
    {
        public override IEnumerator DoTransition(StackedScreen from, StackedScreen to)
        {
            //remove unused overlays
            //remove screen
            yield return null;
            //show new screen
            //show new overlays
        }
    }
}