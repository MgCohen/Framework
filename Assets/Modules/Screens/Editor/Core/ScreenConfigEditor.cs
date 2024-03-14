using Scaffold.Screens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scaffold.Screens.Core.Editor
{

    [CustomEditor(typeof(ScreenConfig))]
    public class ScreenConfigEditor : UIConfigEditor
    {
        private ReorderableList overlayList;
        
        protected override void Setup()
        {
            base.Setup();
            CreateOverlayList();
        }

        private void CreateOverlayList()
        {
            var listProp = serializedObject.FindProperty("Overlays");
            overlayList = new ReorderableList(serializedObject, listProp, true, false, true, true);

            overlayList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                SerializedProperty prop = listProp.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, prop, true);
            };

            overlayList.elementHeightCallback = (int index) =>
            {
                SerializedProperty prop = listProp.GetArrayElementAtIndex(index);
                return EditorGUI.GetPropertyHeight(prop, true);
            };
        }

        protected override void DrawDefaultProperties()
        {
            base.DrawDefaultProperties();
            DrawOverlayList();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawOverlayList()
        {
            DrawLabel("Overlays");

            if(overlayList == null)
            {
                CreateOverlayList();
            }

            overlayList.DoLayoutList();
        }

        protected override bool ValidateAsset(SerializedProperty assetProp)
        {
            AssetReference asset = assetProp.boxedValue as AssetReference;
            return (asset.editorAsset as GameObject)?.GetComponent<IScreen>() != null;
        }
    }
}
