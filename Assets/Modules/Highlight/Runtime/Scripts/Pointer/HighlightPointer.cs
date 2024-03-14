using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Highlight;

public class HighlightPointer : MonoBehaviour
{
    [SerializeField] private Transform fingerTransform;

    private Tweener tween;

    public void Set(PointerOptions options)
    {
        float rotation = options.Rotation;

        Vector3 rot = new Vector3(0, 0, rotation);
        transform.localRotation = Quaternion.Euler(rot);

        Vector3 fingerRot = new Vector3(0, 0, 45);
        fingerRot.x = (rotation > 90 && rotation < 270) ? 180 : 0;
        fingerTransform.localRotation = Quaternion.Euler(fingerRot);

        if(tween != null)
        {
            tween.Kill();
        }
        tween = transform.DOMove(transform.position + (transform.right * 10f), 0.8f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    private void OnDestroy()
    {
        if (tween != null)
        {
            tween.Kill();
        }
    }
}
