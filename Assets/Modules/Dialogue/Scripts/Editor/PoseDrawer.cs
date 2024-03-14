using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DialogueCharacter.CharacterPose))]
public class PoseDrawer : PropertyDrawer
{

    private float height = (EditorGUIUtility.singleLineHeight * 2) + 05;
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return height;
    }

    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {

        EditorGUI.BeginProperty(rect, GUIContent.none, property);

        Rect position = new Rect(rect);
        position.height = EditorGUIUtility.singleLineHeight;
        position.width -= height + 10;

        var nameProp = property.FindPropertyRelative("pose");
        EditorGUI.PropertyField(position, nameProp);

        position.y += position.height;
        var spriteProp = property.FindPropertyRelative("sprite");
        EditorGUI.PropertyField(position, spriteProp);


        Rect previewRect = new Rect(rect);
        previewRect.x += position.width + 5;
        previewRect.width = height;
        previewRect.height -= 5;
        
        var texture = AssetPreview.GetAssetPreview(spriteProp.objectReferenceValue);
        if (texture != null)
        {
            EditorGUI.DrawTextureTransparent(previewRect, texture, ScaleMode.ScaleToFit);
        }

        EditorGUI.EndProperty();
    }
}
