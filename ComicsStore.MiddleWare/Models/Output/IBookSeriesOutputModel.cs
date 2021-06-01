namespace ComicsStore.MiddleWare.Models.Output
{
    public interface IBookSeriesOutputModel
    {
        int BookId { get; set; }
        string Issue { get; set; }
        int SeriesId { get; set; }
        decimal? SeriesOrder { get; set; }
    }
}