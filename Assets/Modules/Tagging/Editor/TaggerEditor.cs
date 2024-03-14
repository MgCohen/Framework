using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tagger))]
public class TaggerEditor: Editor
{
    public TagList tagList;

    private int selectedTagIndex = 0;

    private GUIStyle TagStyle => new GUIStyle(GUI.skin.button)
    {
        alignment = TextAnchor.MiddleLeft,
    };

    public override void OnInspectorGUI()
    {
        Tagger tagger = serializedObject.targetObject as Tagger;
        var tagsProp = serializedObject.FindProperty("tags");
        var childCount = tagsProp.arraySize;
        for (int i = 0; i < childCount; i++)
        {
            var childProp = tagsProp.GetArrayElementAtIndex(i);
            GameTag tag = childProp.boxedValue as GameTag;
            if(tag == null)
            {
                continue;
            }

            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("X", GUILayout.Width(30)))
            {
                tagsProp.DeleteArrayElementAtIndex(i);
                serializedObject.ApplyModifiedProperties();
                continue;
            }

            var color = GUI.color;
            Color targetColor = tag.Color;
            targetColor.a = 1;
            GUI.color = targetColor;
            if (GUILayout.Button(tag.Tag, TagStyle))
            {

            }
            GUI.color = color;
            EditorGUILayout.EndHorizontal();
        }
            
        EditorGUILayout.BeginHorizontal();
        selectedTagIndex = EditorGUILayout.Popup(selectedTagIndex, tagList?.Tags?.Select(t => t.name).ToArray());
        if(GUILayout.Button("Add Tag"))
        {
            GameTag tagToAdd = tagList.Tags[selectedTagIndex];
            if (tagger.ContainsTag(tagToAdd))
            {
                Debug.LogWarning("Tagger component already contains this tag");
                return;
            }

            tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
            tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1).objectReferenceValue = tagToAdd;
            serializedObject.ApplyModifiedProperties();
        }
        if (GUILayout.Button("New"))
        {
            Selection.activeObject = tagList;
        }
        EditorGUILayout.EndHorizontal();
    }
}
