using UnityEngine;
using Zenject;

public abstract class Highlight
{
    public Highlight(GameObject target, PointerOptions pointerOptions)
    {
        Target = target;
        Options = pointerOptions;
    }

    public GameObject Target { get; private set; }
    public PointerOptions Options { get; private set; }

    [Inject] protected HighlightPointer pointerPrefab;
    protected HighlightPointer pointer;

    [Inject]
    public virtual void Initialize()
    {
        if (Options != null)
        {
            pointer = GameObject.Instantiate(pointerPrefab, Target.transform);
            pointer.Set(Options);
        }
    }

    public virtual void Dispose()
    {
        if(pointer != null)
        {
            GameObject.Destroy(pointer.gameObject);
        }
    }

    public class Factory : PlaceholderFactory<GameObject, PointerOptions, Highlight>
    {

    }
}
