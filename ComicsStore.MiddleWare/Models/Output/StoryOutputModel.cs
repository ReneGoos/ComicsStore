using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryOutputModel : StoryOnlyOutputModel, IStoryOutputModel
    {
        public ICollection<StoryBookOutputModel> StoryBook { get; set; }
        public ICollection<StoryCharacterOutputModel> StoryCharacter { get; set; }
        public ICollection<StoryArtistOutputModel> StoryArtist { get; set; }
    }
}
