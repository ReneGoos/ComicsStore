using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output;

public class BookArtistOutputModel : IBookArtistOutputModel
{
    public int ArtistId { get; set; }
    public int BookId { get; set; }
    public ICollection<string> ArtistType { get; set; }

    public ArtistOnlyOutputModel Artist { get; set; }
}
