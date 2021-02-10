using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class ArtistStoryOutputModel : StoryOnlyOutputModel
    {
        public int StoryId { get; set; }
        public List<string> ArtistType { get; set; }
    }
}
