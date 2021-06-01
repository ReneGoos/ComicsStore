using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input
{
    public interface IStoryArtistInputModel : IBasicCrossInputModel
    {
        int ArtistId { get; set; }
        ICollection<string> ArtistType { get; set; }
        int StoryId { get; set; }
    }
}