using UnityEditor;
using UnityEngine;

public class GameTag : ScriptableObject
{
    public string Tag => tag;
    [SerializeField, Delayed] private string tag;
    public Color Color => color;
    [SerializeField] private Color color = Color.black;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (this == null) return;

        if(color.a == 0)
        {
            color = Random.ColorHSV();
        }

        SerializedObject serializedObj = new SerializedObject(this);
        serializedObj.targetObject.name = tag;
        if (!AssetDatabase.IsAssetImportWorkerProcess())
        {
            string path = AssetDatabase.GetAssetPath(this);
            if (!string.IsNullOrWhiteSpace(path))
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
#endif
}
