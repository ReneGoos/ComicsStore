using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class SeriesOutputModel : SeriesOnlyOutputModel
    {
        public ICollection<SeriesBookOutputModel> BookSeries { get; set; }
    }
}