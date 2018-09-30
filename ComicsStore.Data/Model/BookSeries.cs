using System;

namespace ComicsStore.Data.Model
{
    public class BookSeries
    {
        public BookSeries()
        {
            CreationDate = DateTime.Now;
            DateUpdate = DateTime.Now;
        }

        public int BookId { get; set; }
        public string SeriesNr { get; set; }
        public int? SeriesOrder { get; set; }
        public int SeriesId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateUpdate { get; set; }

        public Book Book { get; set; }
        public Series Series { get; set; }
    }
}