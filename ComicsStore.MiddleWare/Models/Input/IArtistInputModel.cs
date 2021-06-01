using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input
{
    public interface IArtistInputModel : IBasicInputModel
    {
        ICollection<StoryArtistInputModel> StoryArtist { get; set; }
    }
}