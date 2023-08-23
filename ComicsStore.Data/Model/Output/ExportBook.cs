using ComicsStore.Data.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicsStore.Data.Model.Output
{
    public class ExportBook : ResultView
    {
        public int StoryId { get; set; }
        public int BookId { get; set; }
        public int SeriesId { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        [Column("Story number")]
        public int? StoryNumber { get; set; }
        public string ExtraInfo { get; set; }
        [Column("Type")]
        public StoryType StoryType { get; set; }
        public BookType BookType { get; set; }
        public string Character { get; set; }
        public string StoryCode { get; set; }
        public string Artist { get; set; }
        public ArtistType ArtistType { get; set; }
        public string Issue { get; set; }
        [Column("Issue title")]
        public string IssueTitle { get; set; }
        public string Language { get; set; }
        public string Series { get; set; }
        public string Publisher { get; set; }
        public int? Year { get; set; }
        [Column("Purchase date")]
        public DateTime PurchaseDate { get; set; }
        public string Notes { get; set; }
        public Active Deleted { get; set; }
    }
}
