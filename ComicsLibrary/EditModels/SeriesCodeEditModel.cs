namespace ComicsLibrary.EditModels
{
    public class SeriesCodeEditModel : CrossEditModel
    {
        private int _seriesId;
        private int _codeId;

        public int CodeId { get => _codeId; set { Set(ref _codeId, value); } }
        public int SeriesId { get => _seriesId; set { Set(ref _seriesId, value); } }
    }
}