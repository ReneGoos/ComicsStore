namespace ComicsStore.MiddleWare.Models.Output
{
    public class CodeSeriesOutputModel : SeriesOnlyOutputModel, ICodeSeriesOutputModel
    {
        public int SeriesId { get; set; }
    }
}