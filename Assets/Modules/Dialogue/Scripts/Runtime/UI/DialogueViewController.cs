using System;
using UnityEngine;

public abstract class DialogueViewController: MonoBehaviour
{
    public abstract void NextLine(LineMoment line, bool blockInput, Action callback);
}
