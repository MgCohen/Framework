
#if DOTWEEN
using DG.Tweening;
#endif

using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class OpenCloseTransition : SceneTransition
{
    [SerializeField] private RectTransform whiteBar;
    [SerializeField] private RectTransform blackBar;
    [SerializeField] private Transform holder;

    [SerializeField] private float animationDuration;
    [SerializeField] private Vector3 rotation;

    private Vector3 whiteInitialPos;
    private Vector3 blackInitialPos;

#if DOTWEEN
    public override async Task OutAnimation()
    {
        whiteInitialPos = whiteBar.transform.position;
        whiteBar.DOMove(holder.position, animationDuration);

        blackInitialPos = blackBar.transform.position;
        await blackBar.DOMove(holder.position, animationDuration).AsyncWaitForCompletion();
    }


    protected override async Task LoadingAnimation()
    {
        await holder.DORotate(rotation, animationDuration, RotateMode.WorldAxisAdd).SetEase(Ease.InOutCirc).AsyncWaitForCompletion();
    }

    public override async Task InAnimation()
    {
        whiteBar.DOMove(whiteInitialPos, animationDuration);
        await blackBar.DOMove(blackInitialPos, animationDuration).AsyncWaitForCompletion();
    }
#endif
}
