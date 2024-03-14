using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ZenAutoInjecter))]
public abstract class SceneTransition : MonoBehaviour
{
    [Inject] ISceneServices scenes;

    public bool TransitionDone { get; private set; }

    [Inject]
    private void SetAsCurrentTransition()
    {
        scenes.SetCurrentTransition(this);
    }

    public virtual Task OutAnimation()
    {
        return Task.CompletedTask;
    }

    public async void DoLoadingAnimation()
    {
        TransitionDone = false;
        await LoadingAnimation();
        TransitionDone = true;
    }

    protected virtual Task LoadingAnimation()
    {
        return Task.CompletedTask;
    }

    public virtual Task InAnimation()
    {
        return Task.CompletedTask;
    }
}
