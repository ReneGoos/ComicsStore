using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class StoryArtistInputModel : BasicCrossInputModel
    {
        public int StoryId { get; set; }
        public int ArtistId { get; set; }
        public ICollection<string> ArtistType { get; set; }
    }
}
