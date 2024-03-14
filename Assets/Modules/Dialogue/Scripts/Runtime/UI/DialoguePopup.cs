using DG.Tweening;
using Scaffold.Screens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DialoguePopup : Screen<DialoguePopupContext>
{
    [Inject] private DialogueViewFactory factory;

    [SerializeField] private Image background;
    [SerializeField] private float timeBetweenMoments = 0.5f;

    private DialogueViewController controller;
    private List<Moment> moments;
    private int currentMoment;
    private DialogueStyle currentStyle;

    private float nextMomentTimestamp;

    protected override void Setup()
    {
        base.Setup();
        var color = background.color;
        color.a = 0;
        background.color = color;
        OnContextUpdated();
    }

    protected override void OnContextUpdated()
    {
        SetBackground(Context.Dialogue.Fadeout);
        SpawnDialogueController(Context.Dialogue.Style);
        SetMoments();
        NextMoment();
    }

    private void SetMoments()
    {
        moments = Context.Dialogue.Moments.ToList();
        currentMoment = 0;
        nextMomentTimestamp = 0;
    }

    private void SpawnDialogueController(DialogueStyle style)
    {
        if(currentStyle == style && controller != null)
        {
            return;
        }

        if(controller != null)
        {
            Destroy(controller.gameObject);
        }
        currentStyle = style;
        controller = factory.Create(style, content);
    }

    private void SetBackground(bool fadeout)
    {
        float fade = fadeout ? 0.7f : 0;
        //background.DOFade(fade, 0.7f);
    }

    public void NextMoment()
    {
        if (Time.time < nextMomentTimestamp)
        {
            return;
        }

        if(currentMoment >= moments.Count)
        {
            Context.OnComplete?.Invoke();
            return;
        }

        nextMomentTimestamp = Time.time + timeBetweenMoments;
        Moment nextMoment = moments[currentMoment++];
        if(nextMoment is LineMoment line)
        {
            controller.NextLine(line, Context.Dialogue.BlockInput, NextMoment);
        }
        else if(nextMoment is EventMoment ev)
        {
            ev.Trigger(NextMoment);
        }
    }
}


public class DialoguePopupContext : ScreenContext<DialoguePopup>
{
    public DialoguePopupContext(Dialogue dialogue, Action onComplete)
    {
        Dialogue = dialogue;
        OnComplete = onComplete;
    }

    public Dialogue Dialogue { get; private set; }
    public Action OnComplete { get; private set; }

    public void SwapDialogue(Dialogue dialogue)
    {
        Dialogue = dialogue;
        NotifyContentUpdate();
    }
}
