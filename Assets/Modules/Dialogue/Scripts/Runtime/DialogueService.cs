using Scaffold.Screens;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DialogueService : IDialogueService
{
    [Inject] private IScreenService screens;
    private DialoguePopup popup;
    private DialoguePopupContext context;

    public void ShowDialogue(Dialogue dialogue, Action onComplete)
    {
        if (context != null)
        {
            context.SwapDialogue(dialogue);
            return;
        }

        context = new DialoguePopupContext(dialogue, OnDialogueComplete);
        popup = screens.Open<DialoguePopup>(context, false);

        void OnDialogueComplete()
        {
            if (context.Dialogue.CloseOnLastLine)
            {
                CloseDialogue();
            }
            onComplete?.Invoke();
        }
    }
    public void CloseDialogue()
    {
        if (popup != null)
        {
            screens.Close(popup);
            context = null;
            popup = null;
        }
    }
}
