using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueCharacterCollection : ScriptableObject
{
    public IReadOnlyList<DialogueCharacter> Characters => characters;
    [SerializeField] private List<DialogueCharacter> characters;


#if UNITY_EDITOR
    [Button("Fetch All Characters")]
    private void FetchCharacters()
    {
        var guids = UnityEditor.AssetDatabase.FindAssets($"t:{nameof(DialogueCharacter)}");
        characters = guids.Select(guid => UnityEditor.AssetDatabase.LoadAssetAtPath<DialogueCharacter>(UnityEditor.AssetDatabase.GUIDToAssetPath(guid))).ToList();
    }
#endif
}
