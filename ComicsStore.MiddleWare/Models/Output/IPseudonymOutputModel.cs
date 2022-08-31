namespace ComicsStore.MiddleWare.Models.Output;

public interface IPseudonymOutputModel
{
    int MainArtistId { get; set; }
    int PseudonymArtistId { get; set; }
}