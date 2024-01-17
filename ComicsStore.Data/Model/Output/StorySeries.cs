using ComicsStore.Data.Common;
using System;

namespace ComicsStore.Data.Model.Output
{
    public class StorySeries : ResultView
    {
        public int StoryId { get; set; }
        public int BookId { get; set; }
        public int SeriesId { get; set; }
        public int SeriesCodeId { get; set; }
        public int StoryCodeId { get; set; }
        public string StoryName { get; set; }
        public string OriginalStoryName { get; set; }
        public decimal? StoryNumber { get; set; }
        public string ExtraInfo { get; set; }
        public StoryType StoryType { get; set; }
        public BookType BookType { get; set; }
        public string CharacterName { get; set; }
        public string StoryCode { get; set; }
        public string SeriesCode { get; set; }
        public string ArtistName { get; set; }
        public ArtistType ArtistType { get; set; }
        public string Issue { get; set; }
        public string IssueTitle { get; set; }
        public string Language { get; set; }
        public string SeriesName { get; set; }
        public string PublisherName { get; set; }
        public int? Year { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Active Deleted { get; set; }
        public int PublisherId { get; set; }
        public int MinSeriesOrder { get; set; }
        public int MaxSeriesOrder { get; set; }
    }
}
