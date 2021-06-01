using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class SeriesOutputModel : SeriesOnlyOutputModel, ISeriesOutputModel
    {
        public ICollection<SeriesBookOutputModel> BookSeries { get; set; }
    }
}