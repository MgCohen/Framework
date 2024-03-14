using System.Collections.Generic;
using Zenject;
using System.Linq;

public class TagManager
{
    [Inject] private TagList tagList;

    private Dictionary<GameTag, List<Tagger>> taggedObjects = new Dictionary<GameTag, List<Tagger>>();

    public void RegisterTaggedObject(Tagger tagger)
    {
        foreach(var tag in tagger.Tags)
        {
            RegisterTaggedObject(tagger, tag);
        }
    }

    private void RegisterTaggedObject(Tagger tagger, GameTag tag)
    {
        if(!taggedObjects.TryGetValue(tag, out var list))
        {
            list = new List<Tagger>();
            taggedObjects[tag] = list;
        }

        if (list.Contains(tagger))
        {
            return;
        }

        list.Add(tagger);
        list.RemoveAll(t => t == null);
    }

    public void DiscardTaggedObject(Tagger tagger)
    {
        foreach (var tag in tagger.Tags)
        {
            DiscardTaggedObject(tagger, tag);
        }
    }

    private void DiscardTaggedObject(Tagger tagger, GameTag tag)
    {
        if (tag == null) return;

        if(!taggedObjects.TryGetValue(tag, out var list))
        {
            return;
        }

        list.RemoveAll(t => t == null || t == tagger);
    }

    public List<Tagger> GetAllTaggedObjects(GameTag tag)
    {
        if (!taggedObjects.TryGetValue(tag, out var list))
        {
            list = new List<Tagger>();
            taggedObjects[tag] = list;
        }
        list.RemoveAll(t => t == null);
        return list;
    }

    public List<Tagger> GetAllTaggedObjects(string tag)
    {
        GameTag gameTag = StringToTag(tag);
        if(gameTag == null)
        {
            return new List<Tagger>();
        }
        return GetAllTaggedObjects(gameTag);
    }

    private GameTag StringToTag(string tag)
    {
        return tagList.Tags.FirstOrDefault(t => t.Tag == tag);
    }

}
