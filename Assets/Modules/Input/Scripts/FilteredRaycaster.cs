using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class FilteredRaycaster: GraphicRaycaster
{
    [Inject] private InputService input;

    public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
    {
        base.Raycast(eventData, resultAppendList);
        input.FilterRaycast(eventData, resultAppendList);
    }
}