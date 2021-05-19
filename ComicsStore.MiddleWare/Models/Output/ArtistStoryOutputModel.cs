using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class ArtistStoryOutputModel : StoryOnlyOutputModel
    {
        public int ArtistId { get; set; }
        public int StoryId { get; set; }
        public ICollection<string> ArtistType { get; set; }
    }
}
