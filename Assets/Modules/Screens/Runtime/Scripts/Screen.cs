using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Scaffold.Screens
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasScaler))]
    public abstract class Screen : MonoBehaviour, IScreen
    {
        public List<ScreenComponent> Components => components;
        [SerializeField, HideInInspector] private List<ScreenComponent> components = new List<ScreenComponent>();

        [SerializeField, HideInInspector] protected Canvas canvas;
        [SerializeField, HideInInspector] protected RectTransform content;

        private bool initialized = false;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (canvas == null)
            {
                canvas = GetComponent<Canvas>();
            }

            if (content == null)
            {
                content = transform.GetChild(0) as RectTransform;
            }
        }
#endif
        public ScreenType ScreenType => screenType;
        [SerializeField] private ScreenType screenType;

        public int Layer => canvas.sortingOrder;

        protected virtual void Setup()
        {
            components = GetComponents<ScreenComponent>().ToList();
            foreach (var component in components)
            {
                component.Setup(this);
            }
        }

        public IEnumerator Open()
        {
            if (!initialized)
            {
                Setup();
                initialized = true;
            }

            foreach (var component in components)
            {
                component.OnOpen(true);
            }
            yield return OnOpen();
        }

        protected virtual IEnumerator OnOpen()
        {
            yield return null;
        }

        public IEnumerator Close()
        {
            foreach (var component in components)
            {
                component.OnClose(true);
            }
            yield return OnClose();
        }

        protected virtual IEnumerator OnClose()
        {
            yield return null;
        }

        public void Focus()
        {
            gameObject.SetActive(true);
            foreach (var component in components)
            {
                component.OnOpen(false);
            }
            OnFocus();
        }

        protected virtual void OnFocus() { }

        public void Hide()
        {
            foreach (var component in components)
            {
                component.OnClose(false);
            }
            OnHide();
            gameObject.SetActive(false);
        }

        protected virtual void OnHide() { }

        public void SetLayer(int layer)
        {
            canvas.sortingOrder = layer;
        }

    }

    public abstract class Screen<T> : Screen, IScreenT where T : IScreenContext
    {
        protected T Context;

        public void SetContext(IScreenContext context)
        {
            if (context is not T)
            {
                throw new System.Exception("Trying to set the wrong context type on screen");
            }
            Context = (T)context;
            Context.ContextUpdated -= OnContextUpdated;
            Context.ContextUpdated += OnContextUpdated;
        }

        protected virtual void OnContextUpdated() { }
    }
}
