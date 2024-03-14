using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueBox: MonoBehaviour
{
    [SerializeField] private RectTransform boxRect;

    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI dialogueContent;
    [SerializeField] private RectTransform backgroundRect;


    public void SetText(LineMoment line)
    {
        SetBoxPosition(line.Position);
        characterName.text = line.Character.Character;
        dialogueContent.text = line.Content;
    }

    private void SetBoxPosition(DialoguePosition position)
    {
        bool isTop = position is DialoguePosition.TopLeft or DialoguePosition.TopRight;
        bool isLeft = position is DialoguePosition.TopLeft or DialoguePosition.BottomLeft;

        if (isTop) boxRect.SetAsFirstSibling();
        else boxRect.SetAsLastSibling();

        var yAnchor = isTop ? 1 : 0;
        var xScale = isLeft ? 1 : -1f;
        var yPos = isTop ? -200 : 200;

        boxRect.anchorMin = new Vector2(0, yAnchor);
        boxRect.anchorMax = new Vector2(1, yAnchor);
        boxRect.pivot = new Vector2(0.5f, yAnchor);
        boxRect.anchoredPosition = new Vector2(boxRect.anchoredPosition.x, yPos);

        var scale = backgroundRect.localScale;
        scale.x = xScale;
        backgroundRect.localScale = scale;
    }
}