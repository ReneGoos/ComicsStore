namespace ComicsStore.MiddleWare.Models.Output
{
    public class BookSeriesOutputModel : BasicCrossOutputModel
    {
        public int SeriesId { get; set; }
        public string Issue { get; set; }
        public decimal? SeriesOrder { get; set; }

        public SeriesOnlyOutputModel Series { get; set; }
    }
}