using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input;

public interface IBookArtistInputModel : IBasicCrossInputModel
{
    int ArtistId { get; set; }
    ICollection<string> ArtistType { get; set; }
    int BookId { get; set; }
}