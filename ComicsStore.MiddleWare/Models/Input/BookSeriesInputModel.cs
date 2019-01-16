using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class BookSeriesInputModel : BasicCrossInputModel
    {
        public int SeriesId { get; set; }
        public string Issue { get; set; }
        public decimal? SeriesOrder { get; set; }

        public SeriesInputModel Series { get; set; }
    }
}