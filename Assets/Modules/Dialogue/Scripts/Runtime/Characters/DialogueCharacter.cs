using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueCharacter : ScriptableObject
{
    public string Character => character;
    [SerializeField] private string character;

    public IReadOnlyList<CharacterPose> Poses => sprites;
    [SerializeField] private List<CharacterPose> sprites;

    public CharacterPose GetPose(string pose)
    {
        return Poses.FirstOrDefault(p => p.Pose == pose);
    }

    [Serializable]
    public class CharacterPose
    {
        public string Pose => pose;
        [SerializeField] private string pose;

        public Sprite Sprite => sprite;
        [SerializeField] private Sprite sprite;
    }
}
