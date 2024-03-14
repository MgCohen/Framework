using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputExtensions
{
    public static void FilterForTag(this InputService input, GameTag tag)
    {
        input.SetInputFilter((rh) =>
        {
            Tagger tagger = rh.gameObject.GetComponent<Tagger>();
            return tagger && tagger.ContainsTag(tag);
        }, true);
    }

    public static void FilterForTags(this InputService input, bool matchAll, params GameTag[] tags)
    {
        input.SetInputFilter((rh) =>
        {
            Tagger tagger = rh.gameObject.GetComponent<Tagger>();
            if (!tagger)
            {
                return false;
            }

            return tagger && tagger.ContainsTag(tags, matchAll);
        }, true);
    }
}
