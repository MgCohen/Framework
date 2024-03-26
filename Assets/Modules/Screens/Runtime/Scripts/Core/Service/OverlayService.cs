using System.Linq;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace Scaffold.Screens.Core
{
    public class OverlayService
    {
        private ScreenFactory factory;
        private ScreenTransitionController screenQueue;
        private Transform overlayHolder;
        private List<IScreen> currentOverlays = new List<IScreen>();

        //public IEnumerator RemoveUnusedOverlays(ScreenConfig fromScreen, ScreenConfig toScreen)
        //{
        //    List<IScreen> overlaysToRemove = new List<IScreen>();
        //    int animationCounter = 0;
        //    foreach (var overlay in currentOverlays)
        //    {
        //        if (!toScreen.HasOverlay(overlay))
        //        {
        //            overlaysToRemove.Add(overlay);
        //            animationCounter++;
        //            OverlayConfig config = fromScreen.GetConfig(overlay);
        //            screenQueue.StartSequenceWithCallback(config.OutAnimation.Animate(overlay, true), () =>
        //            {
        //                animationCounter--;
        //            });
        //        }
        //    }
        //    yield return new WaitUntil(() => animationCounter <= 0);
        //    currentOverlays = currentOverlays.Except(overlaysToRemove).ToList();
        //}

        //public IEnumerator OpenMissingOverlays(ScreenConfig toScreen)
        //{
        //    int animationCounter = 0;
        //    foreach (var config in toScreen.Overlays)
        //    {
        //        if (!currentOverlays.Any(overlay => overlay.GetType() == config.Type))
        //        {
        //            IScreen overlay = factory.Create(config.Type, overlayHolder);
        //            currentOverlays.Add(overlay);
        //            animationCounter++;
        //            screenQueue.StartSequenceWithCallback(config.InAnimation.Animate(overlay), () =>
        //            {
        //                animationCounter--;
        //            });
        //        }
        //    }

        //    yield return new WaitUntil(() => animationCounter <= 0);
        //}
    }
}
