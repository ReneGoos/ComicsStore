namespace ComicsStore.MiddleWare.Models.Output
{
    public class PseudonymArtistOutputModel : IPseudonymOutputModel
    {
        public int MainArtistId { get; set; }
        public int PseudonymArtistId { get; set; }

        public ArtistOnlyOutputModel MainArtist { get; set; }
    }
}