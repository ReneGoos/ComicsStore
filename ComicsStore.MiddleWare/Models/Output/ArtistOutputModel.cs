using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class ArtistOutputModel : ArtistOnlyOutputModel, IArtistOutputModel
    {
        public ICollection<ArtistStoryOutputModel> StoryArtist { get; set; }
        public ICollection<ArtistPseudonymOutputModel> MainArtist { get; set; }
        public ICollection<PseudonymArtistOutputModel> PseudonymArtist { get; set; }
    }
}
