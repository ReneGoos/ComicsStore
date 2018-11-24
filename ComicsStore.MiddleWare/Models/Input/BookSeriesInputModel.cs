using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class BookSeriesInputModel
    {
        public int SeriesId { get; set; }
        public string SeriesNr { get; set; }
        public int? SeriesOrder { get; set; }

        public SeriesInputModel Series { get; set; }
    }
}