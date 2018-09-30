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
        sequential = 0,
        single = 1,
        gag = 2,
        cartoon = 3
    }

    public enum ArtistType
    {
        artist = 0,
        penciller = 1,
        inker = 2,
        colorist = 3,
        writer = 4,
        writer_artist = 5,
        writer_penciller = 6,
        writer_inker = 7,
        writer_colorist = 8,
        inspiration = 9
    }

}