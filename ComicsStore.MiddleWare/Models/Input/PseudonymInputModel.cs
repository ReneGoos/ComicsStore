namespace ComicsStore.MiddleWare.Models.Input
{
    public class PseudonymInputModel : BasicCrossInputModel, IPseudonymInputModel
    {
        public int MainArtistId { get; set; }
        public int PseudonymArtistId { get; set; }
    }
}
