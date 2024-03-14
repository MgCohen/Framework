using Scaffold.Schemas;
using Scaffold.Types;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scaffold.Screens.Core
{
    public abstract class UIConfig: SchemaObject
    {
        public AssetReference Asset => asset;
        [SerializeField] protected AssetReference asset;
        public Type Type => type.Type;
        [SerializeField] protected TypeReference type;

        public ScreenAnimation InAnimation => inAnimation;
        [SerializeField] protected ScreenAnimation inAnimation;
        public ScreenAnimation OutAnimation => outAnimation;
        [SerializeField] protected ScreenAnimation outAnimation;

#if UNITY_EDITOR

        private void OnValidate()
        {
            type = new TypeReference((asset?.editorAsset as GameObject)?.gameObject?.GetComponent<IScreen>()?.GetType());
        }
#endif
    }

}