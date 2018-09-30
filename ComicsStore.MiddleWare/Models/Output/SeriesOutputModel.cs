using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class SeriesOutputModel : BasicOutputModel
    {
        public string SeriesName { get; set; }
        public int? SeriesNumber { get; set; }
        public string SeriesLanguage { get; set; }

        public CodeOutputModel SeriesCode { get; set; }
        public ICollection<SeriesBookOutputModel> BookSeries { get; set; }
    }
}