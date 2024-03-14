#if DOTWEEN
using DG.Tweening;
#endif

using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SimpleSceneTransition : SceneTransition
{
    [SerializeField] private bool doOutAnimation = true;
    [SerializeField] private bool doInAnimation = true;

    [SerializeField] private Image blackout;
    [SerializeField] private float fadeDuration;

#if DOTWEEN
    public override async Task OutAnimation()
    {
        if (!doOutAnimation)
        {
            return;
        }

        await blackout.DOFade(1, fadeDuration).From(0).AsyncWaitForCompletion();
    }

    public override async Task InAnimation()
    {
        if (!doInAnimation)
        {
            return;
        }

        await blackout.DOFade(0, fadeDuration).From(1).AsyncWaitForCompletion();
    }
#endif
}
