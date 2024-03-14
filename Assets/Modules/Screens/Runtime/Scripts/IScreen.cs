using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scaffold.Screens
{

    public interface IScreen
    {
        public List<ScreenComponent> Components { get; }
        public GameObject gameObject { get; }
        public ScreenType ScreenType { get; }
        public int Layer { get; }

        public IEnumerator Open();
        public void Focus();
        public IEnumerator Close();
        public void Hide();
        public void SetLayer(int layer);
    }

    public interface IScreenT : IScreen
    {
        public void SetContext(IScreenContext context);
    }

    public enum ScreenType
    {
        Window = 0,
        Popup = 1,
        Tab = 2,
        Overlay = 3,
    }
}
