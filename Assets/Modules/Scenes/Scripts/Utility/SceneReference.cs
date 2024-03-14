#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using UnityEngine.ResourceManagement.ResourceProviders;

[Serializable]
public class SceneReference
{
    public SceneReference(string name)
    {
        SceneName = name;
    }

    public string SceneName;
#if UNITY_EDITOR
    public SceneAsset Scene;
#endif
    public SceneInstance Instance;

    public static implicit operator string(SceneReference scene) => scene.SceneName;
}
