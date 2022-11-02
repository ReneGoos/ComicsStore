using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class ArtistStoryOutputModel : IArtistStoryOutputModel
    {
        public int ArtistId { get; set; }
        public int StoryId { get; set; }
        public ICollection<string> ArtistType { get; set; }

        public StoryOnlyOutputModel Story { get; set; }
    }
}
