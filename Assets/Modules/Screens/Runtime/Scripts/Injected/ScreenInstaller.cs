using Scaffold.Screens.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Scaffold.Screens.Injected
{
    public class ScreenInstaller : MonoInstaller
    {
        [SerializeField] private ScreenSettings screens;

        public override void InstallBindings()
        {
            Container.Bind<IScreenService>().To<ScreenService>().AsSingle().NonLazy();
            Container.Bind<IScreenFactory>().To<ScreenFactoryWithInjection>().AsTransient();
            Container.Bind<IScreenProvider>().To<ScreenProvider>().AsSingle().NonLazy();
            Container.Bind<ScreenStack>().AsSingle();
            Container.Bind<ScreenAnimationController>().FromNewComponentOnNewGameObject().AsTransient();
            Container.BindInterfacesAndSelfTo<ScreenSettings>().FromInstance(screens);

            //look through all signals?
            //Container.DeclareSignal<ToggleUIElementSignal>().OptionalSubscriber();
            //Container.DeclareSignal<ToggleHeaderSignal>().OptionalSubscriber();
            Container.Bind<ScreenServiceSignalHandler>().AsSingle().NonLazy();
            Container.DeclareSignal<ScreenChangedSignal>().OptionalSubscriber();
        }
    }
}
