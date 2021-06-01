namespace ComicsStore.MiddleWare.Models.Input
{
    public interface IBookSeriesInputModel : IBasicCrossInputModel
    {
        int BookId { get; set; }
        string Issue { get; set; }
        int SeriesId { get; set; }
        decimal? SeriesOrder { get; set; }
    }
}