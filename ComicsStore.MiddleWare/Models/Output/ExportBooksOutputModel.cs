using System;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class ExportBooksOutputModel
    {
        public string Title { get; set; }
        public int? StoryNumber { get; set; }
        public string ExtraInfo { get; set; }
        public string StoryType { get; set; }
        public string BookType { get; set; }
        public string Character { get; set; }
        public string Artist { get; set; }
        public string ArtistType { get; set; }
        public string Issue { get; set; }
        public string IssueTitle { get; set; }
        public string Language { get; set; }
        public string Series { get; set; }
        public string Publisher { get; set; }
        public int? Year { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Notes { get; set; }
        public string Deleted { get; set; }
    }
}
