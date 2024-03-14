using System;
using UnityEngine;

[Serializable]
public abstract class Moment
{

}

[Serializable]
public class LineMoment : Moment
{
    public DialogueCharacter Character => character;
    [SerializeField] private DialogueCharacter character;

    public DialogueCharacter.CharacterPose Pose => character.GetPose(pose);
    [SerializeField] private string pose;

    public string Content => content;
    [SerializeField] private string content;

    public DialoguePosition Position => position;
    [SerializeField] private DialoguePosition position;
}

public abstract class EventMoment: Moment
{
    public abstract void Trigger(Action callback);
}

[Serializable]
public class Transition : EventMoment
{
    public override void Trigger(Action callback)
    {
    }
}
