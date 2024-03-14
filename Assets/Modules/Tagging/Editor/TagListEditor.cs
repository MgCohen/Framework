using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(TagList))]
public class TagListEditor : Editor
{
    ReorderableList list;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("Tags"), true, false, false, false);
        list.drawElementCallback = DrawElement;
        list.elementHeight = EditorGUIUtility.singleLineHeight;
    }

    private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        try
        {
            SerializedProperty element = list.serializedProperty?.GetArrayElementAtIndex(index);
            SerializedObject nestedElement = new SerializedObject(element.objectReferenceValue);

            var nameProp = nestedElement.FindProperty("tag");
            var halfRect = new Rect(rect);
            halfRect.width = (rect.width - 30) / 2;

            EditorGUI.PropertyField(halfRect, nameProp, GUIContent.none);

            var colorProp = nestedElement.FindProperty("color");
            halfRect.x += halfRect.width;
            EditorGUI.PropertyField(halfRect, colorProp, GUIContent.none);

            Rect buttonRect = new Rect(rect);
            buttonRect.x += buttonRect.width - 30;
            buttonRect.width = 30;
            if (GUI.Button(buttonRect, "X"))
            {
                serializedObject.FindProperty("Tags").DeleteArrayElementAtIndex(index);
                AssetDatabase.RemoveObjectFromAsset(nestedElement.targetObject);
                serializedObject.ApplyModifiedProperties();
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            nestedElement.ApplyModifiedProperties();
        }
        catch { }
    }

    public override void OnInspectorGUI()
    {
        try
        {
            list?.DoLayoutList();
        }
        catch { }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Tag"))
        {
            var tagsProp = serializedObject.FindProperty("Tags");
            tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
            tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1).objectReferenceValue = CreateTag();
            serializedObject.ApplyModifiedProperties();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

    private GameTag CreateTag()
    {
        var newTag = CreateInstance<GameTag>();
        AssetDatabase.AddObjectToAsset(newTag, serializedObject.targetObject);
        return newTag;
    }
}
