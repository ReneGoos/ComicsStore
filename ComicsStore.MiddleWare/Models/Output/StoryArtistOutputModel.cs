using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryArtistOutputModel : BasicCrossOutputModel
    {
        public int ArtistId { get; set; }
        public List<string> ArtistType { get; set; }

        public ArtistOnlyOutputModel Artist { get; set; }
    }
}
