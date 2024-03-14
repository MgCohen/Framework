using System.Collections.Generic;
using UnityEngine;
using Scaffold.Schemas;
using System.Linq;

namespace Scaffold.Screens.Core
{
    [CreateAssetMenu(menuName = "Scaffold/Screens/Screen Config")]
    public class ScreenConfig : UIConfig
    {
        public List<OverlayConfig> Overlays = new List<OverlayConfig>();
        
        //[Serializable]
        //public class OverlayOption
        //{
        //    public AssetReference Asset => asset;
        //    [SerializeField] private AssetReference asset;

        //    public Type Type => type.Type;
        //    [SerializeField] private TypeReference type;

        //    [SerializeReference] public IOverlayConfig Config;
        //}

        public bool HasOverlay(IScreen overlay)
        {
            return Overlays.Any(oc => oc.Type == overlay.GetType());
        }

        public OverlayConfig GetConfig(IScreen overlay)
        {
            return Overlays.FirstOrDefault(oc => oc.Type == overlay.GetType());
        }
    }

}