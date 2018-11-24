using System;

namespace ComicsStore.Data.Model
{
    public class BookSeries : CrossTable
    {
        public BookSeries()
            : base()
        {
        }

        public int BookId { get; set; }
        public string Issue { get; set; }
        public int? SeriesOrder { get; set; }
        public int SeriesId { get; set; }

        public Book Book { get; set; }
        public Series Series { get; set; }
    }
}