namespace ComicsStore.MiddleWare.Models.Output
{
    public class SeriesBookOutputModel : BasicBookOutputModel
    {
        public string Issue { get; set; }
        public decimal? SeriesOrder { get; set; }
    }
}
