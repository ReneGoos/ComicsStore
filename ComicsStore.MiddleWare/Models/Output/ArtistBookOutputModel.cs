using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output;

public class ArtistBookOutputModel : IArtistBookOutputModel
{
    public int ArtistId { get; set; }
    public int BookId { get; set; }
    public ICollection<string> ArtistType { get; set; }

    public BookOnlyOutputModel Book { get; set; }
}
