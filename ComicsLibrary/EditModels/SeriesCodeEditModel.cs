namespace ComicsLibrary.EditModels
{
    public class SeriesCodeEditModel : CrossEditModel
    {
        private int _seriesId;
        private int _codeId;

        public int CodeId { get => _codeId; set { _codeId = value; RaisePropertyChanged(); } }
        public int SeriesId { get => _seriesId; set { _seriesId = value; RaisePropertyChanged(); } }
    }
}