using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

public class Tagger : MonoBehaviour
{
    [Inject] private TagManager tagManager;

    public IReadOnlyList<GameTag> Tags => tags;
    [SerializeField] private List<GameTag> tags = new List<GameTag>();

    [Inject]
    private void OnEnable()
    {
        tagManager?.RegisterTaggedObject(this);
    }

    private void OnDisable()
    {
        tagManager?.DiscardTaggedObject(this);
    }

    public bool ContainsTag(GameTag tag)
    {
        return tags.Contains(tag);
    }

    public bool ContainsTag(GameTag[] tags, bool matchAll)
    {
        if (matchAll)
        {
            return this.tags.All(t => tags.Any(t2 => t == t2));
        }
        else
        {
            return tags.Any(t => ContainsTag(t));
        }
    }
}
