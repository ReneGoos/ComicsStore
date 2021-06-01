using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public interface IStoryOutputModel
    {
        ICollection<StoryArtistOutputModel> StoryArtist { get; set; }
        ICollection<StoryBookOutputModel> StoryBook { get; set; }
        ICollection<StoryCharacterOutputModel> StoryCharacter { get; set; }
    }
}