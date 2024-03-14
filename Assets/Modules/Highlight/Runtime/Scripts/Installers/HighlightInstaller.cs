using UnityEngine;
using Zenject;

public class HighlightInstaller: MonoInstaller
{
    [SerializeField] private HighlightPointer pointer;
    public override void InstallBindings()
    {
        Container.Bind<IHighlightService>().To<HighlightService>().AsSingle();
        Container.Bind<HighlightPointer>().FromInstance(pointer);
        Container.BindFactory<GameObject, PointerOptions, Highlight, Highlight.Factory>().FromFactory<HighlightFactory>();
    }
}