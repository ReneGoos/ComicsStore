namespace ComicsStore.MiddleWare.Models.Input
{
    public class BookSeriesInputModel : BasicCrossInputModel, IBookSeriesInputModel
    {
        public int BookId { get; set; }
        public int SeriesId { get; set; }
        public string Issue { get; set; }
        public decimal? SeriesOrder { get; set; }
    }
}