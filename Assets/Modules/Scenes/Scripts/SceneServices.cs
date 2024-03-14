using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneServices : ISceneServices
{
    [Inject] private SceneTransitionList transitions;
    [Inject] private SignalBus signals;

    private SceneReference currentScene;
    private SceneTransition currentTransition;


    public void SetCurrentTransition(SceneTransition transition)
    {
        currentTransition = transition;
    }

    public async Task LoadScene(string to)
    {
        SceneReference sceneReference = new SceneReference(to);
        await LoadScene(sceneReference);
    }

    public async Task LoadScene(SceneReference to)
    {
        string from = currentScene ?? SceneManager.GetActiveScene().name;
        var transition = transitions.GetTransition(from, to);
        await DoSceneTransition(currentScene, transition, to);
    }

    private async Task DoSceneTransition(SceneReference fromScene, SceneReference transitionScene, SceneReference toScene)
    {
        signals.Fire(new SceneTransitionSignal(TransitionState.LoadingOut, fromScene, toScene));
        await WaitSceneLoad(transitionScene);
        while (currentTransition == null)
        {
            //wait for the transition code to start and trigger from the scene
            await Task.Yield();
        }

        signals.Fire(new SceneTransitionSignal(TransitionState.AnimatingOut, fromScene, toScene));
        await currentTransition.OutAnimation();
        signals.Fire(new SceneTransitionSignal(TransitionState.Transition, fromScene, toScene));
        currentTransition.DoLoadingAnimation();
        await UnloadCurrentScene();

        signals.Fire(new SceneTransitionSignal(TransitionState.LoadingIn, fromScene, toScene));
        await WaitSceneLoad(toScene, () => currentTransition.TransitionDone);
        currentScene = toScene;

        signals.Fire(new SceneTransitionSignal(TransitionState.AnimatingIn, fromScene, toScene));
        await currentTransition.InAnimation();
        signals.Fire(new SceneTransitionSignal(TransitionState.Complete, fromScene, toScene));
        await Addressables.UnloadSceneAsync(transitionScene.Instance).Task;
        currentTransition = null;
    }

    private async Task UnloadCurrentScene()
    {
        if(currentScene == null)
        {
            var scene = SceneManager.GetActiveScene();
            var unload = SceneManager.UnloadSceneAsync(scene);
            while (!unload.isDone)
            {
                await Task.Yield();
            }
        }
        else
        {
            await Addressables.UnloadSceneAsync(currentScene.Instance).Task;
        }
    }

    private async Task WaitSceneLoad(SceneReference sceneReference, Func<bool> readyPredicate = null)
    {
        bool needsValidation = readyPredicate != null;
        var loadingOperation = Addressables.LoadSceneAsync(sceneReference.SceneName, LoadSceneMode.Additive, needsValidation);
        var scene = await loadingOperation.Task;
        if (needsValidation)
        {
            while (!readyPredicate.Invoke())
            {
                await Task.Yield();
            }
        }
        var activation = loadingOperation.Result.ActivateAsync();
        while (!activation.isDone)
        {
            await Task.Yield();
        }
        sceneReference.Instance = scene;
    }
}

public class SceneTransitionSignal
{
    public SceneTransitionSignal(TransitionState state, SceneReference from, SceneReference to)
    {
        State = state;
        From = from;
        To = to;
    }

    public TransitionState State { get; }
    public SceneReference From { get; }
    public SceneReference To { get; }
}

public enum TransitionState
{
    LoadingOut = 0,
    AnimatingOut = 1,
    Transition = 2,
    LoadingIn = 3,
    AnimatingIn = 4,
    Complete = 5,
}