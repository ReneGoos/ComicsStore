namespace ComicsStore.MiddleWare.Models.Input;

public interface IPseudonymInputModel
{
    int MainArtistId { get; set; }
    int PseudonymArtistId { get; set; }
}