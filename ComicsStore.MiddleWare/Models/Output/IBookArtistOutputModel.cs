using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output;

public interface IBookArtistOutputModel
{
    int ArtistId { get; set; }
    ICollection<string> ArtistType { get; set; }
    int BookId { get; set; }
}