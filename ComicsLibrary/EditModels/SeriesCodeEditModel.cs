namespace ComicsLibrary.EditModels
{
    public class SeriesCodeEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _seriesId;
        private int? _codeId;
        private CodeOnlyEditModel _code;

        public int? CodeId { get => _codeId; set => SetIfValue(ref _codeId, value); }
        public int? SeriesId { get => _seriesId; set => SetIfValue(ref _seriesId, value); }
        public CodeOnlyEditModel Code { get => _code; set => SetIfValue(ref _code, value); }

        public int? MainId { get => SeriesId; set => SeriesId = value; }
        public int? LinkedId { get => CodeId; set => CodeId = value; }
        public TableEditModel ChildItem { get => Code; set => Code = value as CodeOnlyEditModel; }
    }
}