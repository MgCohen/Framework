using System.Collections;
using UnityEngine;

namespace Scaffold.Screens
{
    [CreateAssetMenu(menuName = "Scaffold/Screens/Animations/FadeIn")]
    public class FadeInScreenAnimation : ScreenAnimation
    {
        [SerializeField] private float animTime;

        public override IEnumerator Animate(IScreen screen, bool backwards = false)
        {
            screen.gameObject.SetActive(true);
            CanvasGroup group = screen.gameObject.GetComponent<CanvasGroup>();
            bool createdGroupForAnimation = true;
            if (group == null)
            {
                createdGroupForAnimation = false;
                group = screen.gameObject.AddComponent<CanvasGroup>();
            }
            group.alpha = backwards ? 1 : 0;
            float currentAlpha = 0;
            while (currentAlpha < 1)
            {
                currentAlpha += (1/animTime) * Time.deltaTime;
                group.alpha = backwards ? 1 - currentAlpha : currentAlpha;
                yield return null;
            }

            if (!createdGroupForAnimation)
            {
                Destroy(group);
            }
        }
    }
}