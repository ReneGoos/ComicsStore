using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComicsStore.Data.Model
{
    public class ExportMemento
    {
        public string Title { get; set; }
        [Column("Story number")]
        public int? StoryNumber { get; set; }
        public string ExtraInfo { get; set; }
        [Column("Type")]
        public StoryType StoryType { get; set; }
        public BookType BookType { get; set; }
        public string Character { get; set; }
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
