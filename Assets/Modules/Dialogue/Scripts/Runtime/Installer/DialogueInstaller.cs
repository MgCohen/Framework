using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DialogueInstaller : MonoInstaller
{
    [SerializeField] private BoxDialogueViewController boxView;

    public override void InstallBindings()
    {
        Container.Bind<BoxDialogueViewController>().FromInstance(boxView);
        Container.Bind<DialogueViewFactory>().AsSingle();
        Container.Bind<IDialogueService>().To<DialogueService>().AsSingle();
    }
}
