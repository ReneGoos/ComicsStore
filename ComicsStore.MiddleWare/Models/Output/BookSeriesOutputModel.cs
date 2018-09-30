using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class BookSeriesOutputModel : BasicOutputModel
    {
        public string SeriesNr { get; set; }
        public int? SeriesOrder { get; set; }
        public string SeriesName { get; set; }
        public int? SeriesNumber { get; set; }
        public string SeriesLanguage { get; set; }
    }
}