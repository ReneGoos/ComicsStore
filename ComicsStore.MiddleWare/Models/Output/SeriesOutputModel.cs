using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class SeriesOutputModel : BasicOutputModel
    {
        public int? SeriesNumber { get; set; }
        public string SeriesLanguage { get; set; }

        public int CodeId { get; set; }
        public CodeOutputModel Code { get; set; }

        public ICollection<SeriesBookOutputModel> BookSeries { get; set; }
    }
}