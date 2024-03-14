using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LineMoment))]
public class LineMomentDrawer : PropertyDrawer
{
    public DialogueCharacterCollection characterCollection;

    private float height = (EditorGUIUtility.singleLineHeight * 3) + 05;
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return height;
    }

    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        if(characterCollection == null)
        {
            var guid = UnityEditor.AssetDatabase.FindAssets($"t:{nameof(DialogueCharacterCollection)}").First();
            characterCollection = UnityEditor.AssetDatabase.LoadAssetAtPath<DialogueCharacterCollection>(UnityEditor.AssetDatabase.GUIDToAssetPath(guid));
        }

        EditorGUI.BeginProperty(rect, GUIContent.none, property);

        Rect characterLine = new Rect(rect);
        characterLine.width /= 3;
        characterLine.height = EditorGUIUtility.singleLineHeight;

        var positionProp = property.FindPropertyRelative("position");
        EditorGUI.PropertyField(characterLine, positionProp, GUIContent.none);
        
        characterLine.x += characterLine.width;
        var characterProp = property.FindPropertyRelative("character");
        int selectedIndex = characterCollection.Characters.ToList().IndexOf((DialogueCharacter)characterProp.objectReferenceValue);
        selectedIndex = EditorGUI.Popup(characterLine, selectedIndex, characterCollection.Characters.Select(c => c.Character).ToArray());
        DialogueCharacter selectedCharacter = characterCollection.Characters[(selectedIndex < 0 ? 0 : selectedIndex)];
        characterProp.objectReferenceValue = selectedCharacter;

        characterLine.x += characterLine.width;
        var poseProp = property.FindPropertyRelative("pose");
        selectedIndex = selectedCharacter.Poses.ToList().IndexOf(selectedCharacter.GetPose(poseProp.stringValue));
        selectedIndex = EditorGUI.Popup(characterLine, selectedIndex, selectedCharacter.Poses.Select(p => p.Pose).ToArray());
        poseProp.stringValue = selectedCharacter.Poses[(selectedIndex < 0 ? 0 : selectedIndex)].Pose;

        Rect contentLine = new Rect(rect);
        contentLine.height = 2 * EditorGUIUtility.singleLineHeight;
        contentLine.y += characterLine.height + 2f;
        var contentProp = property.FindPropertyRelative("content");
        EditorGUI.PropertyField(contentLine, contentProp, GUIContent.none);

        EditorGUI.EndProperty();
    }
}
