namespace ComicsStore.MiddleWare.Models.Output
{
    public class CodeSeriesOutputModel : ICodeSeriesOutputModel
    {
        public int SeriesId { get; set; }

        public SeriesOnlyOutputModel Series { get; set; }
    }
}