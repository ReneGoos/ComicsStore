namespace ComicsStore.MiddleWare.Models.Output
{
    public class ArtistPseudonymOutputModel : IPseudonymOutputModel
    {
        public int MainArtistId { get; set; }
        public int PseudonymArtistId { get; set; }

        public ArtistOnlyOutputModel PseudonymArtist { get; set; }
    }
}