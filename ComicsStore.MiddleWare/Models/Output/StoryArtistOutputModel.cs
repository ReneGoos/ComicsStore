using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryArtistOutputModel : ArtistOnlyOutputModel
    {
        public int ArtistId { get; set; }
        public ICollection<string> ArtistType { get; set; }
    }
}
