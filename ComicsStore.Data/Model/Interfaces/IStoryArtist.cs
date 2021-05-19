using System.Collections.Generic;

namespace ComicsStore.Data.Model.Interfaces
{
    public interface IStoryArtist
    {
        ICollection<StoryArtist> StoryArtist { get; set; }
    }
}