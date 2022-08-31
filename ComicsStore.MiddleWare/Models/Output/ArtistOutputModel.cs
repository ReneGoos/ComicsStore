using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class ArtistOutputModel : ArtistOnlyOutputModel, IArtistOutputModel
    {
        public ICollection<ArtistStoryOutputModel> StoryArtist { get; set; }
        public ICollection<PseudonymOutputModel> MainArtist { get; set; }
        public ICollection<PseudonymOutputModel> PseudonymArtist { get; set; }
    }
}
