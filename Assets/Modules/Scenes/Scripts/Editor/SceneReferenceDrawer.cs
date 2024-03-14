using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SceneReference))]
public class SceneReferenceDrawer: PropertyDrawer
{

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var rect = EditorGUI.PrefixLabel(position, label);
        var prop = property.FindPropertyRelative(nameof(SceneReference.Scene));
        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(rect, prop, GUIContent.none);
        if (EditorGUI.EndChangeCheck())
        {
            var sceneName = (prop.boxedValue as SceneAsset)?.name;
            property.FindPropertyRelative(nameof(SceneReference.SceneName)).stringValue = sceneName;
        }
    }
}
