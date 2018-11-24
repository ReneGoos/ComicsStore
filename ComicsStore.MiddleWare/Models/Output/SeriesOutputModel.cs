using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class SeriesOutputModel : BasicOutputModel
    {
        public int? SeriesNumber { get; set; }
        public string SeriesLanguage { get; set; }

        public CodeOutputModel SeriesCode { get; set; }
        public ICollection<SeriesBookOutputModel> BookSeries { get; set; }
    }
}