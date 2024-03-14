using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TagInstaller : MonoInstaller
{
    [SerializeField] private TagList tagList;

    public override void InstallBindings()
    {
        Container.Bind<TagList>().FromInstance(tagList);
        Container.BindInterfacesAndSelfTo<TagManager>().AsSingle();
    }

}
