namespace ComicsLibrary.EditModels
{
    public class ArtistPseudonymEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _mainArtistId;
        private int? _pseudonymArtistId;

        public int? MainArtistId { get => _mainArtistId; set => SetIfValue(ref _mainArtistId, value); }
        public int? PseudonymArtistId { get => _pseudonymArtistId; set => SetIfValue(ref _pseudonymArtistId, value); }
        public ArtistOnlyEditModel PseudonymArtist { get; set; }

        public int? MainId { get => MainArtistId; set => MainArtistId = value; }
        public int? LinkedId { get => PseudonymArtistId; set => PseudonymArtistId = value; }
        public TableEditModel ChildItem { get => PseudonymArtist; set => PseudonymArtist = value as ArtistOnlyEditModel; }
    }
}