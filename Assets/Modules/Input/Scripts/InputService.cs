using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class InputService : MonoBehaviour, ITickable
{
    [Inject] private SignalBus signals;

    private List<Func<RaycastResult, bool>> filters = new List<Func<RaycastResult, bool>>();
    private List<RaycastResult> hits = new List<RaycastResult>();
    private bool screenClickedThisFrame = false;
    private bool inputBlocked = false;

    public void FilterRaycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
    {
        if (inputBlocked)
        {
            resultAppendList.Clear();
            return;
        }

        if(filters.Count > 0)
        {
            resultAppendList.RemoveAll(hit => !FilterRaycastHit(hit));
        }

        if (Input.GetMouseButtonUp(0))
        {
            hits.AddRange(resultAppendList);
            screenClickedThisFrame = true;
        }
    }

    private bool FilterRaycastHit(RaycastResult hit)
    {
        foreach(var filter in filters)
        {
            if (filter.Invoke(hit))
            {
                return true;
            }
        }
        return false;
    }

    public void SetInputFilter(Func<RaycastResult, bool> predicate, bool removeOtherFilters = false)
    {
        if (removeOtherFilters)
        {
            filters.Clear();
        }
        filters.Add(predicate);
    }

    public void ToggleInput(bool toggle)
    {
        inputBlocked= !toggle;
    }


    public void ClearFilters()
    {
        filters.Clear();
    }

    public void Tick()
    {
        if (screenClickedThisFrame)
        {
            screenClickedThisFrame = false;
            signals.Fire(new ScreenClickedSignal(hits, hits.FirstOrDefault()));
            hits.Clear();
        }
    }
}

public class ScreenClickedSignal
{
    public ScreenClickedSignal(List<RaycastResult> results, RaycastResult topResult)
    {
        Results = results;
        TopResult = topResult;
    }

    public List<RaycastResult> Results {get; private set; }
    public RaycastResult TopResult { get; private set; }
}
