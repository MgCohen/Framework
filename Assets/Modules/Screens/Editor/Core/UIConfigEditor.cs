using Scaffold.Schemas.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scaffold.Screens.Core.Editor
{
    [CustomEditor(typeof(UIConfig), true)]
    public class UIConfigEditor : SchemaObjectEditor
    {
        protected override void DrawDefaultProperties()
        {
            DrawAssetPicker();
            DrawAnimations();
        }

        private void DrawAssetPicker()
        {
            DrawLabel("Screen Asset");

            EditorGUILayout.BeginHorizontal();
            SerializedProperty assetProp = serializedObject.FindProperty("asset");
            AssetReference assetRef = assetProp.boxedValue as AssetReference;

            EditorGUILayout.PropertyField(assetProp, GUIContent.none);

            EditorGUI.BeginDisabledGroup(true);
            SerializedProperty typeProp = serializedObject.FindProperty("type");
            EditorGUILayout.PropertyField(typeProp, GUIContent.none, GUILayout.Width(150));
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();

            GameObject asset = assetRef.editorAsset as GameObject;
            if (asset == null)
            {
                EditorGUILayout.HelpBox("Asset is required, please select one", MessageType.Error);
            }
            else if (asset.GetComponent<Screen>() == null)
            {
                EditorGUILayout.HelpBox("selected asset is neither a Screen or Overlay, please select the correct asset", MessageType.Error);
            }
        }

        private void DrawAnimations()
        {
            DrawLabel("Animations");

            SerializedProperty inProp = serializedObject.FindProperty("inAnimation");
            EditorGUILayout.PropertyField(inProp, new GUIContent("In"));
            SerializedProperty outProp = serializedObject.FindProperty("outAnimation");
            EditorGUILayout.PropertyField(outProp, new GUIContent("Out"));
        }


        protected void DrawLabel(string label)
        {
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            var rect = EditorGUILayout.GetControlRect(false, 1f);
            EditorGUI.DrawRect(rect, Color.white);
            EditorGUILayout.Space(2);
        }

        protected virtual bool ValidateAsset(SerializedProperty assetProp)
        {
            AssetReference asset = assetProp.boxedValue as AssetReference;
            return (asset.editorAsset as GameObject).GetComponent<IScreen>() != null;
        }
    }
}
