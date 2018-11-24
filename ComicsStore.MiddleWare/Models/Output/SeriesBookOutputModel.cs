using ComicsStore.Data.Model;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class SeriesBookOutputModel : BasicBookOutputModel
    {
        public string SeriesNr { get; set; }
        public int? SeriesOrder { get; set; }
    }
}
