using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public interface ISeriesOutputModel
    {
        int? SeriesNumber { get; set; }
        string SeriesLanguage { get; set; }

        int CodeId { get; set; }
        CodeOutputModel Code { get; set; }

        ICollection<SeriesBookOutputModel> BookSeries { get; set; }
    }
}