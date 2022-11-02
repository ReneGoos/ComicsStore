using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryArtistOutputModel : IStoryArtistOutputModel
    {
        public int ArtistId { get; set; }
        public int StoryId { get; set; }
        public ICollection<string> ArtistType { get; set; }

        public ArtistOnlyOutputModel Artist { get; set; }
    }
}
