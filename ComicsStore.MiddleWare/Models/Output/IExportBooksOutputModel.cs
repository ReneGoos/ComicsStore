using System;

namespace ComicsStore.MiddleWare.Models.Output
{
    public interface IExportBooksOutputModel
    {
        string Artist { get; set; }
        string ArtistType { get; set; }
        string BookType { get; set; }
        string Character { get; set; }
        string Deleted { get; set; }
        string ExtraInfo { get; set; }
        string Issue { get; set; }
        string IssueTitle { get; set; }
        string Language { get; set; }
        string Notes { get; set; }
        string OriginalTitle { get; set; }
        string Publisher { get; set; }
        DateTime PurchaseDate { get; set; }
        string Series { get; set; }
        string SeriesCode { get; set; }
        string StoryCode { get; set; }
        int? StoryNumber { get; set; }
        string StoryType { get; set; }
        string Title { get; set; }
        int? Year { get; set; }
    }
}