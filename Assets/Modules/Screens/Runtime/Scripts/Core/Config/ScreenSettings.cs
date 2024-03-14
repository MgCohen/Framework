using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Scaffold.Types;
using System.Linq;

namespace Scaffold.Screens.Core
{

    [CreateAssetMenu(menuName = "Game/Screens/Dictionary")]
    public class ScreenSettings : ScriptableObject
    {
        [SerializeField] private List<ScreenConfig> screens = new List<ScreenConfig>();
        [SerializeField] private List<OverlayConfig> overlays = new List<OverlayConfig>();

        public bool TryGetScreenConfig(Type type, out ScreenConfig config)
        {
            config = screens.FirstOrDefault(s => s.Type.IsAssignableFrom(type));
            return config != null;
        }

        public bool TryGetOverlayConfig(Type type, out OverlayConfig config)
        {
            config = overlays.FirstOrDefault(s => s.Type.IsAssignableFrom(type));
            return config != null;
        }
    }
}
