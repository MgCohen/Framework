using System.Collections;
using UnityEngine;

namespace Scaffold.Screens
{
    public abstract class ScreenTransition: ScriptableObject
    {
        public abstract IEnumerator DoTransition();
    }
}