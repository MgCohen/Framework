using Scaffold.Screens.Core;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Scaffold.Screens.Injected
{
    public class ScreenFactoryWithInjection : IScreenFactory
    {
        public ScreenFactoryWithInjection(DiContainer container, ScreenSettings screenDictionary)
        {
            this.container = container;
            this.screenDictionary = screenDictionary;
        }

        private DiContainer container;
        private ScreenSettings screenDictionary;

        public IScreen Create(Type screenType, Transform screenHolder)
        {
            if(!screenDictionary.TryGetScreenConfig(screenType, out ScreenConfig config))
            {
                throw new Exception($"There is no screen defined of type {screenType.Name}");
            }

            var handle = Addressables.LoadAssetAsync<GameObject>(config.Asset);
            var prefab = handle.WaitForCompletion();

            //var container = ScaffoldSceneContext.CurrentContainer ?? Container;
            var instance = container.InstantiatePrefabForComponent<Screen>(prefab);

            instance.transform.SetParent(screenHolder);
            instance.transform.SetAsFirstSibling(); //for now, to guarantee that it is on top
            return instance;
        }

    }
}