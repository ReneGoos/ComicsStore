using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class ArtistOutputModel : ArtistOnlyOutputModel
    {
        public ICollection<ArtistStoryOutputModel> StoryArtist { get; set; }
    }
}
