using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class ArtistOutputModel : BasicOutputModel
    {
        public ICollection<ArtistStoryOutputModel> StoryArtist { get; set; }
    }
}
