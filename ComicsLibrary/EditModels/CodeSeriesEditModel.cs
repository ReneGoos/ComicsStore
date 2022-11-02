using ComicsStore.Data.Model;

namespace ComicsLibrary.EditModels
{
    public class CodeSeriesEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _seriesId;
        private int? _codeId;

        public int? CodeId { get => _codeId; set => SetIfValue(ref _codeId, value); }
        public int? SeriesId { get => _seriesId; set => SetIfValue(ref _seriesId, value); }
        public SeriesOnlyEditModel Series { get; set; }

        public int? MainId { get => CodeId; set => CodeId = value; }
        public int? LinkedId { get => SeriesId; set => SeriesId = value; }
        public TableEditModel ChildItem { get => Series; set => Series = value as SeriesOnlyEditModel; }
    }
}