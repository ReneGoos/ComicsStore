using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input;

public interface IArtistInputModel : IBasicInputModel
{
    ICollection<BookArtistInputModel> BookArtist { get; set; }
    ICollection<StoryArtistInputModel> StoryArtist { get; set; }
}