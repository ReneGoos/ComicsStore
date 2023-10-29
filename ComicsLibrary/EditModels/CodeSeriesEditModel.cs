using ComicsLibrary.EditModels.Interfaces;

namespace ComicsLibrary.EditModels
{
    public class CodeSeriesEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _seriesId;
        private int? _codeId;
        private SeriesOnlyEditModel _series;

        public int? CodeId { get => _codeId; set => SetIfValue(ref _codeId, value); }
        public int? SeriesId { get => _seriesId; set => SetIfValue(ref _seriesId, value); }
        public SeriesOnlyEditModel Series { get => _series; set => SetIfValue(ref _series, value); }

        public int? MainId { get => CodeId; set => CodeId = value; }
        public int? LinkedId { get => SeriesId; set => SeriesId = value; }
        public TableEditModel ChildItem { get => Series; set => Series = value as SeriesOnlyEditModel; }
    }
}