namespace ComicsStore.Data.Model.Interfaces;

public interface IMainArtist
{
    ICollection<Pseudonym> MainArtist { get; set; }
}