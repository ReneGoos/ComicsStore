namespace ComicsLibrary.EditModels
{
    public class ArtistPseudonymEditModel : CrossEditModel
    {
        private int? _mainArtistId;
        private int? _pseudonymArtistId;

        public int? MainArtistId { get => _mainArtistId; set => SetIfValue(ref _mainArtistId, value); }
        public int? PseudonymArtistId { get => _pseudonymArtistId; set => SetIfValue(ref _pseudonymArtistId, value); }

        public override int? MainId { get => MainArtistId; set => MainArtistId = value; }
        public override int? LinkedId { get => PseudonymArtistId; set => PseudonymArtistId = value; }
    }
}