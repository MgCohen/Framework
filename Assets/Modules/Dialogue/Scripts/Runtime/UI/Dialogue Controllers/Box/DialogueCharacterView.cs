using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueCharacterView: MonoBehaviour
{

    [SerializeField] private RectTransform characterAnchor;
    [SerializeField] private RectTransform characterRect;
    [SerializeField] private Image sprite;

    public void SetCharacter(LineMoment line)
    {
        sprite.sprite = line.Pose.Sprite;
        SetCharacterPosition(line.Position);
    }

    private void SetCharacterPosition(DialoguePosition position)
    {
        bool isLeft = position is DialoguePosition.TopLeft or DialoguePosition.BottomLeft;
        bool isTop = position is DialoguePosition.TopLeft or DialoguePosition.TopRight;
        var yAnchor = isTop ? 1 : 0;
        var yPos = isTop ? -1250 : 1250;
        var xScale = isLeft ? 1 : -1;


        var scale = characterAnchor.localScale;
        scale.x = xScale;
        characterAnchor.localScale = scale;

        characterRect.anchorMin = new Vector2(-0.05f, yAnchor);
        characterRect.anchorMax = new Vector2(0.5f, yAnchor);
        characterRect.pivot = new Vector2(0, yAnchor);
        characterRect.anchoredPosition = new Vector2(characterRect.anchoredPosition.x, yPos);
    }
}
