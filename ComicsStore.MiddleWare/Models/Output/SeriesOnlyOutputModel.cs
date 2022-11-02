namespace ComicsStore.MiddleWare.Models.Output
{
    public class SeriesOnlyOutputModel : BasicOutputModel
    {
        public int? SeriesNumber { get; set; }
        public string SeriesLanguage { get; set; }

        public int CodeId { get; set; }
    }
}