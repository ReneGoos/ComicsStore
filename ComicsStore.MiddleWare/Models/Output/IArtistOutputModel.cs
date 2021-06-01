using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public interface IArtistOutputModel : IBasicOutputModel
    {
        ICollection<ArtistStoryOutputModel> StoryArtist { get; set; }
    }
}