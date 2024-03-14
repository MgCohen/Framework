using UnityEngine;
using Zenject;

public class DialogueViewFactory
{
    [Inject] private BoxDialogueViewController boxController;
    [Inject] private DiContainer container;
    
    public DialogueViewController Create(DialogueStyle style, Transform parent)
    {
        return container.InstantiatePrefabForComponent<DialogueViewController>(boxController, parent);
    }
}