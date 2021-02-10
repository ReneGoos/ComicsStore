﻿namespace ComicsStore.MiddleWare.Models.Output
{
    public class BookSeriesOutputModel : SeriesOnlyOutputModel
    {
        public int SeriesId { get; set; }
        public string Issue { get; set; }
        public decimal? SeriesOrder { get; set; }
    }
}