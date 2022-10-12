namespace ComicsLibrary.EditModels
{
    public class PseudonymArtistEditModel : ArtistPseudonymEditModel
    {
        public override int? MainId{ get => PseudonymArtistId; set => PseudonymArtistId = value; }
        public override int? LinkedId { get => MainArtistId; set => MainArtistId = value; }
    }
}