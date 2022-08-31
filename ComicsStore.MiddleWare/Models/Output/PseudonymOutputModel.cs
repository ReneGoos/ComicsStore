namespace ComicsStore.MiddleWare.Models.Output
{
    public class PseudonymOutputModel : ArtistOnlyOutputModel, IPseudonymOutputModel
    {
        public int MainArtistId { get; set; }
        public int PseudonymArtistId { get; set; }
    }
}