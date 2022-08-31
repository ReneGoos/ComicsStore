namespace ComicsLibrary.EditModels
{
    public class PseudonymEditModel : CrossEditModel
    {
        private int? _mainArtistId;
        private int? _pseudonymArtistId;

        public int? MainArtistId { get => _mainArtistId; set => Set(ref _mainArtistId, value); }
        public int? PseudonymArtistId { get => _pseudonymArtistId; set => Set(ref _pseudonymArtistId, value); }
    }
}