namespace ComicsStore.MiddleWare.Models.Output
{
    public class SeriesBookOutputModel : BookOnlyOutputModel
    {
        public int BookId { get; set; }
        public int SeriesId { get; set; }
        public string Issue { get; set; }
        public decimal? SeriesOrder { get; set; }
    }
}
