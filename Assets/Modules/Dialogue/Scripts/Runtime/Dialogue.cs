using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Dialogue : ScriptableObject
{
    public string Key => key;
    [SerializeField] private string key;

    public DialogueStyle Style => style;
    [SerializeField] private DialogueStyle style;

    public bool CloseOnLastLine => closeOnLastLine;
    [SerializeField] private bool closeOnLastLine;

    public bool BlockInput => blockInput;
    [SerializeField] private bool blockInput;

    public bool Fadeout => fadeout;
    [SerializeField] private bool fadeout;

    public DynamicList<Moment> Moments => moments;
    [SerializeField, Space(10)] private DynamicList<Moment> moments = new DynamicList<Moment>();
}

public enum DialogueStyle
{
    Box = 0,
    Bubble = 1,
}

public enum DialoguePosition
{
    TopLeft = 0,
    TopRight = 1,
    BottomLeft = 2,
    BottomRight = 3
}