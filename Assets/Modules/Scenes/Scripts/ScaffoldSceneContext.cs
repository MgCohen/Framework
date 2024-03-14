using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class ScaffoldSceneContext : SceneContext
{
    public static DiContainer CurrentContainer;

    protected override void RunInternal()
    {
        base.RunInternal();
        CurrentContainer = Container;
    }
}
