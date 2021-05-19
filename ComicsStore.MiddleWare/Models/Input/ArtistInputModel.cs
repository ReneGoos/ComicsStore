using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class ArtistInputModel : BasicInputModel
    {
        public ICollection<StoryArtistInputModel> StoryArtist { get; set; }
    }
}
