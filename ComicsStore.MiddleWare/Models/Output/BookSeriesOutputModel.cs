namespace ComicsStore.MiddleWare.Models.Output
{
    public class BookSeriesOutputModel
    {
        public int SeriesId { get; set; }
        public string SeriesNr { get; set; }
        public int? SeriesOrder { get; set; }

        public SeriesOnlyOutputModel Series { get; set; }
    }
}