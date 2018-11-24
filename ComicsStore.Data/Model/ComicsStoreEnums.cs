using System;

namespace ComicsStore.Data.Model
{
    public enum Active
    {
        active = 0,
        deleted = 1
    }

    public enum BookType
    {
        book = 0,
        collection = 1,
        periodical = 2
    }

    public enum StoryType
    {
        story = 0,
        one = 1,
        gag = 2,
        cartoon = 3
    }

    [Flags]
    public enum ArtistType
    {
        artist = 1,
        writer = 2,
        penciller = 4,
        inker = 8,
        colorist = 16,
        teacher = 32
    }
}