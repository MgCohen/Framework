using Scaffold.Screens.Core;
using System.Collections;
using UnityEngine;

namespace Scaffold.Screens
{
    public abstract class ScreenTransition: ScriptableObject
    {
        public virtual IEnumerator DoTransition(TransitionType type, StackedScreen from, StackedScreen to)
        {
            if(type is TransitionType.Full or TransitionType.OutOnly)
            {
                yield return OutTransition(from);
            }

            if(type is TransitionType.Full or TransitionType.InOnly)
            {
                yield return InTransition(to);
            }
        }

        protected abstract IEnumerator OutTransition(StackedScreen from);

        protected abstract IEnumerator InTransition(StackedScreen to);
    }

    public class SimpleTransition : ScreenTransition
    {
        protected override IEnumerator InTransition(StackedScreen to)
        {
            bool temporaryGroup = false;
            CanvasGroup group = to.ScreenObject.GetComponent<CanvasGroup>();
            if(group == null)
            {
                temporaryGroup = true;
                group = to.ScreenObject.AddComponent<CanvasGroup>();
            }

            group.alpha = 0;
            while(group.alpha <= 1)
            {
                group.alpha += Time.deltaTime;
                yield return null;
            }

            if (temporaryGroup)
            {
                Destroy(group);
            }
        }

        protected override IEnumerator OutTransition(StackedScreen from)
        {
            bool temporaryGroup = false;
            CanvasGroup group = from.ScreenObject.GetComponent<CanvasGroup>();
            if (group == null)
            {
                temporaryGroup = true;
                group = from.ScreenObject.AddComponent<CanvasGroup>();
            }

            group.alpha = 1;
            while (group.alpha > 0)
            {
                group.alpha -= Time.deltaTime;
                yield return null;
            }

            if (temporaryGroup)
            {
                Destroy(group);
            }
        }
    }

    public enum TransitionType
    {
        Full = 0,
        InOnly = 1,
        OutOnly = 2,
    }
}