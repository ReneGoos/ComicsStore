using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class ExportMementoOutputModel
    {
        public string Title { get; }
        public int? StoryNumber { get; }
        public string ExtraInfo { get; }
        public string StoryType { get; }
        public string BookType { get; }
        public string Character { get; }
        public string Artist { get; }
        public string ArtistType { get; }
        public string Issue { get; }
        public string IssueTitle { get; }
        public string Language { get; }
        public string Series { get; }
        public string Publisher { get; }
        public int? Year { get; }
        public DateTime PurchaseDate { get; }
        public string Notes { get; }
        public string Deleted { get; }
    }
}
