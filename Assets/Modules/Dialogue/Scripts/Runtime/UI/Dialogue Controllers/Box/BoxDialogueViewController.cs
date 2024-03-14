using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoxDialogueViewController : DialogueViewController, IPointerClickHandler
{
    [SerializeField] private DialogueBox box;
    [SerializeField] private DialogueCharacterView character;
    [SerializeField] private Image inputBlocker;

    private Action onClick;

    public override void NextLine(LineMoment line, bool blockInput, Action callback)
    {
        onClick = callback;
        inputBlocker.raycastTarget = blockInput;
        box.SetText(line);
        character.SetCharacter(line);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }
}
