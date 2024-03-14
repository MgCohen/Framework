using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scaffold.Screens.Core
{
    public class ScreenFactory : IScreenFactory
    {
        public ScreenFactory(ScreenSettings settings)
        {
            this.settings = settings;
        }

        private ScreenSettings settings;

        public IScreen Create(Type screenType, Transform screenHolder)
        {
            if (!settings.TryGetScreenConfig(screenType, out ScreenConfig config))
            {
                throw new Exception($"There is no screen defined of type {screenType.Name}");
            }

            var handle = Addressables.LoadAssetAsync<GameObject>(config.Asset);
            var prefab = handle.WaitForCompletion();

            var instance = GameObject.Instantiate(prefab, screenHolder);
            instance.transform.SetAsFirstSibling(); //for now, to guarantee that it is on top
            return instance.GetComponent<IScreen>();
        }
    }
}