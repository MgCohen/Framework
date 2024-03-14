using UnityEngine;
using static Highlight;

public interface IHighlightService
{
    void Highlight(GameObject target, PointerOptions options);
    void RemoveHighlight(GameObject target);
}