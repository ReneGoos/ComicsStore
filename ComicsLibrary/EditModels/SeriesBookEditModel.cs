namespace ComicsLibrary.EditModels
{
    public class SeriesBookEditModel : BookSeriesEditModel
    {
        public override int? MainId { get => SeriesId; set => SeriesId = value; }
        public override int? LinkedId { get => BookId; set => BookId = value; }
    }
}