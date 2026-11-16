namespace ComicsStore.Data.Model.Interfaces;

public interface IBookArtist
{
    ICollection<BookArtist> BookArtist { get; set; }
}