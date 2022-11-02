namespace ComicsLibrary.EditModels
{
    public class PseudonymArtistEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _mainArtistId;
        private int? _pseudonymArtistId;

        public int? MainArtistId { get => _mainArtistId; set => SetIfValue(ref _mainArtistId, value); }
        public int? PseudonymArtistId { get => _pseudonymArtistId; set => SetIfValue(ref _pseudonymArtistId, value); }

        public ArtistOnlyEditModel MainArtist { get; set; }

        public int? MainId{ get => PseudonymArtistId; set => PseudonymArtistId = value; }
        public int? LinkedId { get => MainArtistId; set => MainArtistId = value; }
        public TableEditModel ChildItem { get => MainArtist; set => MainArtist = value as ArtistOnlyEditModel; }
    }
}