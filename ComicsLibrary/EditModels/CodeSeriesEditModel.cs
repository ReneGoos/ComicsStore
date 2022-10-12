namespace ComicsLibrary.EditModels
{
    public class CodeSeriesEditModel : SeriesCodeEditModel
    {
        public override int? MainId { get => CodeId; set => CodeId = value; }
        public override int? LinkedId { get => SeriesId; set => SeriesId = value; }
    }
}