using System;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.Data.Common
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

    public enum PseudonymInd
    {
        yes = 0,
        no = 1
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
        [Display(Name = "inspiration")]
        master = 32,
        letterer = 64,
        translator = 128
    }
}