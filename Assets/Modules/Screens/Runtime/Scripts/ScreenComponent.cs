using UnityEngine;

namespace Scaffold.Screens
{
    public abstract class ScreenComponent : MonoBehaviour
    {
        protected IScreen screen;

        public virtual void Setup(IScreen screen)
        {
            this.screen = screen;
        }

        public virtual void OnOpen(bool isNew)
        {

        }

        public virtual void OnClose(bool isDestroying)
        {

        }
    }
}
