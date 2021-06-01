using ComicsLibrary.Core;
using ComicsLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ComicsLibrary.EditModels
{
    public class ReportEditModel : ObservableObject, ICollectionItem
    {
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public int? StoryNumber { get; set; }
        public string ExtraInfo { get; set; }
        public string StoryType { get; set; }
        public string BookType { get; set; }
        public string Character { get; set; }
        public string StoryCode { get; set; }
        public string SeriesCode { get; set; }
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

        public bool ItemContains(string value)
        {
            if (value is null)
                return true;
            return Title.Contains(value, StringComparison.InvariantCultureIgnoreCase);
        }

        public SortDescriptionCollection ItemSort(List<string> sortKey)
        {
            var sortDescriptions = new SortDescriptionCollection();
            foreach (var key in sortKey)
            {
                sortDescriptions.Add(new SortDescription(key, ListSortDirection.Ascending));
            }

            return sortDescriptions;
        }
    }
}
