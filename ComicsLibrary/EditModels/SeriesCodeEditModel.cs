namespace ComicsLibrary.EditModels
{
    public class SeriesCodeEditModel : CrossEditModel
    {
        private int? _seriesId;
        private int? _codeId;

        public int? CodeId { get => _codeId; set => SetIfValue(ref _codeId, value); }
        public int? SeriesId { get => _seriesId; set => SetIfValue(ref _seriesId, value); }

        public override int? MainId { get => SeriesId; set => SeriesId = value; }
        public override int? LinkedId { get => CodeId; set => CodeId = value; }
    }
}