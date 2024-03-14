//using System;
//using System.Collections.Generic;
//using System.Linq;
//using TypeReferences;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.AddressableAssets;
//using static Scaffold.Screens.Core.ScreenConfig;

//namespace Scaffold.Screens.Core.Editor
//{
//    //[CustomPropertyDrawer(typeof(OverlayOption))]
//    public class OverlayOptionsDrawer : PropertyDrawer
//    {
//        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//        {
//            SerializedProperty configProp = property.FindPropertyRelative(nameof(OverlayOption.Config));
//            if (!property.isExpanded || configProp.boxedValue == null)
//            {
//                return EditorGUIUtility.singleLineHeight;
//            }

//            return EditorGUI.GetPropertyHeight(property, true) + 3f;
//        }

//        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//        {
//            SerializedProperty assetTypeProp = property.FindPropertyRelative("type");
//            SerializedProperty configProp = property.FindPropertyRelative("Config");
//            SerializedProperty assetProp = property.FindPropertyRelative("asset");

//            position.x += 10f;
//            position.width -= 10f;
//            position.height = EditorGUIUtility.singleLineHeight;

//            if (HasConfig(configProp, assetTypeProp))
//            {
//                Rect foldOutPos = EditorGUI.IndentedRect(position);
//                property.isExpanded = EditorGUI.Foldout(foldOutPos, property.isExpanded, GUIContent.none, false);
//            }

//            EditorGUI.BeginChangeCheck();
//            position.y += 1f;
//            EditorGUI.PropertyField(position, assetProp, GUIContent.none);
//            if (EditorGUI.EndChangeCheck())
//            {
//                ResetConfigType(property, assetProp, assetTypeProp);
//            }

//            if (property.isExpanded)
//            {
//                var configProps = GetDirectChildren(configProp);
//                position.y += 3f;
//                foreach(var prop in configProps)
//                {
//                    position.y += position.height;
//                    position.height = EditorGUI.GetPropertyHeight(prop, true);
//                    EditorGUI.PropertyField(position, prop);
//                    position.y += 2f;
//                }
//            }
//        }

//        private bool HasConfig(SerializedProperty configProp, SerializedProperty typeProp)
//        {
//            bool hasConfig = configProp.boxedValue != null;
//            if(!hasConfig && TryGetConfigType(typeProp.boxedValue as TypeReference, out Type configType))
//            {
//                configProp.boxedValue = Activator.CreateInstance(configType);
//                hasConfig = true;
//            }

//            return hasConfig;
//        }

//        private void ResetConfigType(SerializedProperty prop, SerializedProperty assetProp, SerializedProperty typeProp)
//        {
//            prop.FindPropertyRelative("Config").boxedValue = null;
//            typeProp.boxedValue = new TypeReference(((assetProp.boxedValue as AssetReference)?.editorAsset as GameObject)?.GetComponent<IScreen>()?.GetType());
//        }

//        private bool TryGetConfigType(Type elementType, out Type configType)
//        {
//            if (elementType == null)
//            {
//                configType = null;
//                return false;
//            }

//            Type generic = typeof(IOverlayConfig<>);
//            var specific = generic.MakeGenericType(elementType);
//            var implementations = TypeCache.GetTypesDerivedFrom(specific);
//            configType = implementations.FirstOrDefault();
//            return configType != null;
//        }

//        private IEnumerable<SerializedProperty> GetDirectChildren(SerializedProperty parent, int depth = 1)
//        {
//            var cpy = parent.Copy();
//            var depthOfParent = cpy.depth;
//            var enumerator = cpy.GetEnumerator();

//            while (enumerator.MoveNext())
//            {
//                if (enumerator.Current is not SerializedProperty childProperty) continue;
//                if (childProperty.depth > depthOfParent + depth) continue;

//                yield return childProperty.Copy();
//            }
//        }
//    }
//}
