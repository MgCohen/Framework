using System;

public interface IDialogueService
{
    void CloseDialogue();
    void ShowDialogue(Dialogue dialogue, Action onComplete);
}