using System;
using System.ComponentModel.DataAnnotations;

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

    public enum FirstPrint
    {
        yes = 0,
        no = 1
    }

    public enum StoryType
    {
        story = 0,
        one = 1,
        [Display(Name = "Gag")]
        gag = 2,
        cartoon = 3
    }

    [Flags]
    public enum ArtistType
    {
        artist = 1,
        [Display(Name = "Writer")]
        writer = 2,
        penciller = 4,
        inker = 8,
        colorist = 16,
        teacher = 32
    }
}