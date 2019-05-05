using System;

namespace ComicsStore.Data.Model
{
    public class StorySeries
    {
        public int StoryId { get; set; }
        public int BookId { get; set; }
        public int SeriesId { get; set; }
        public int CodeId { get; set; }
        public BookType BookType { get; set; }
        public string Issue { get; set; }
        public string IssueTitle { get; set; }
        public string Language { get; set; }
        public string SeriesName { get; set; }
        public int? Year { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Active Deleted { get; set; }
        public int PublisherId { get; set; }
        public int MinSeriesOrder { get; set; }
        public int MaxSeriesOrder { get; set; }
    }
}
