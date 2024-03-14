using System.Collections;
using UnityEngine;

namespace Scaffold.Screens
{
    public abstract class ScreenAnimation: ScriptableObject
    {
        public abstract IEnumerator Animate(IScreen screen, bool backwards = false);
    }
}