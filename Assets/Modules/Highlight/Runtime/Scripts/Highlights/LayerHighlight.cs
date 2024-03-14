using UnityEngine;
using Zenject;

public class LayerHighlight : Highlight
{
    public LayerHighlight(GameObject target, PointerOptions options, DiContainer container) : base(target, options)
    {
        this.container = container;
    }

    private Canvas canvas;
    private FilteredRaycaster raycaster;
    private DiContainer container;
    
    public override void Initialize()
    {
        base.Initialize(); 
        if (!Target.GetComponent<Canvas>())
        {
            canvas = Target.AddComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingOrder = 999;
        }

        if (!Target.GetComponent<FilteredRaycaster>())
        {
            raycaster = container.InstantiateComponent<FilteredRaycaster>(Target);
        }
    }

    public override void Dispose()
    {
        base.Dispose();
        if (raycaster != null)
        {
            GameObject.Destroy(raycaster);
        }

        if (canvas != null)
        {
            GameObject.Destroy(canvas);
        }
    }
}
