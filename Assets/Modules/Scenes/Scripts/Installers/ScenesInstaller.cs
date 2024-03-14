using UnityEngine;
using Zenject;

public class ScenesInstaller : MonoInstaller
{
    [SerializeField] private SceneTransitionList transitions;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SceneTransitionList>().FromInstance(transitions);
        Container.Bind<ISceneServices>().To<SceneServices>().AsSingle();

        Container.DeclareSignal<SceneTransitionSignal>().OptionalSubscriber();
    }
}