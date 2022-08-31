using System.Collections.Generic;

namespace ComicsStore.Data.Model.Interfaces
{
    public interface IPseudonymArtist
    {
        ICollection<Pseudonym> PseudonymArtist { get; set; }
    }
}