using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input;

public class BookArtistInputModel : BasicCrossInputModel, IBookArtistInputModel
{
    public int BookId { get; set; }
    public int ArtistId { get; set; }
    public ICollection<string> ArtistType { get; set; }
}
