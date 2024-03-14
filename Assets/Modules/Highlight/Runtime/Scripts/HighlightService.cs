using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static Highlight;

public class HighlightService : IHighlightService
{
    [Inject] private Highlight.Factory factory;
    private Dictionary<GameObject, Highlight> activeHighlights = new Dictionary<GameObject, Highlight>();

    public void Highlight(GameObject target, PointerOptions options)
    {
        Highlight highlight = factory.Create(target, options);
        activeHighlights.Add(target, highlight);
    }

    public void RemoveHighlight(GameObject target)
    {
        if (activeHighlights.TryGetValue(target, out Highlight highlight))
        {
            highlight.Dispose();
            activeHighlights.Remove(target);
        }
    }
}
