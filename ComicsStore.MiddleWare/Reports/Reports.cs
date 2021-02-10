using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Common;
using ComicsStore.MiddleWare.Models.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Repositories.Interfaces;

namespace ComicsStore.MiddleWare.Reports
{
    public static class Reports
    {
        public async static Task<string> DataExportAsync(IExportBooksRepository repository, StorySeriesSearchModel searchModel)
        {
            string columnNames = @"""Title"",""Story number"",""Type"",""BookType"",""Character"",""Artist"",""Issue"",""Issue title"",""Language"",""Series"",""Publisher"",""Year"",""Purchase Date"",""Notes"",""Deleted""";

            Active deleted = default;
            string title = null;
            string storyNumber = null;
            StoryType storyType = default;
            BookType bookType = default;
            string bookIssue = null;
            string bookIssueTitle = null;
            string language = null;
            string notes = null;

            int storyId = -1;
            int bookId = -1;
            int seriesId = -1;
            int bookYear = -1;

            DateTime purchaseDate = default;

            var characters = new SortedSet<string>();
            var artists = new SortedList<string, ArtistType>();
            var series = new SortedSet<string>();
            var publishers = new SortedSet<string>();

            StringBuilder exportText = new StringBuilder();

            exportText.AppendLine(columnNames);

            //rsCodes = CodesDataset()
            //SqlDataReader rsStories = BooksDataset(pdbAll, pboolBooks, pboolPeriodicals, pboolDeleted, false);
            foreach (var stories in await repository.GetAsync(searchModel))
            {
                if (storyId == stories.StoryId && bookId == stories.BookId && seriesId == stories.SeriesId)
                {
                    characters.Add(stories.Character);
                    artists[stories.Artist] = (ArtistType)stories.ArtistType;
                    bookIssue = stories.Issue;
                    bookIssueTitle = stories.IssueTitle;
                    language = stories.Language;
                    series.Add(stories.Series);
                    publishers.Add(stories.Publisher);
                }
                else
                {
                    if (title != null)
                    {
                        AddToOutput(deleted,
                            title,
                            storyNumber,
                            storyType,
                            bookType,
                            bookIssue,
                            bookIssueTitle,
                            language,
                            notes,
                            bookYear,
                            purchaseDate,
                            characters,
                            artists,
                            series,
                            publishers,
                            exportText);
                    }

                    characters = new SortedSet<string>();
                    artists = new SortedList<string, ArtistType>();
                    series = new SortedSet<string>();
                    publishers = new SortedSet<string>();

                    storyId = stories.StoryId;
                    bookId = stories.BookId;
                    seriesId = stories.SeriesId;
                    title = stories.Title;
                    storyNumber = !stories.StoryNumber.HasValue ? stories.ExtraInfo : stories.StoryNumber.Value.ToString();
                    storyType = (StoryType)stories.StoryType;
                    bookType = (BookType)stories.BookType;
                    characters.Add(stories.Character);
                    artists[stories.Artist] = (ArtistType)stories.ArtistType;
                    bookIssue = stories.Issue;
                    bookIssueTitle = stories.IssueTitle;
                    language = stories.Language.Length == 0 ? "nl" : stories.Language;
                    series.Add(stories.Series);
                    publishers.Add(stories.Publisher);
                    bookYear = !stories.Year.HasValue ? -1 : stories.Year.Value;
                    purchaseDate = stories.PurchaseDate;
                    deleted = stories.Deleted;
                    notes = stories.Notes;
                }
            }

            AddToOutput(deleted,
                    title,
                    storyNumber,
                    storyType,
                    bookType,
                    bookIssue,
                    bookIssueTitle,
                    language,
                    notes,
                    bookYear,
                    purchaseDate,
                    characters,
                    artists,
                    series,
                    publishers,
                    exportText);

            //    .Visible = True
            //    mdiMain.stbStatus.Panels(1).Text = "Klaar"
            return exportText.ToString();
        }
        
        private static string Escape(string input)
        {
            if (input == null)
                return "";
            return input.Replace('"', '\'');
        }

        private static List<string> ArtistAndShortType(SortedList<string, ArtistType> artists)
        {
            var results = new List<string>();

            foreach (var artist in artists)
            {
                var result = Escape(artist.Key);
                var between = " ";

                foreach (var name in EnumHelper<ArtistType>.GetNames(artist.Value))
                {
                    result += between + name[0].ToString();
                    between = "+";
                }

                results.Add(result);
            }

            return results;
        }

        private static List<string> EscapeList(SortedSet<string> input)
        {
            var results = new List<string>();

            foreach (var name in input)
            {
                results.Add(Escape(name));
            }

            return results;
        }

        private static void AddToOutput(Active deleted,
            string title,
            string storyNumber,
            StoryType storyType,
            BookType bookType,
            string bookIssue,
            string bookIssueTitle,
            string language,
            string notes,
            int bookYear,
            DateTime purchaseDate,
            SortedSet<string> characters, 
            SortedList<string,ArtistType> artists, 
            SortedSet<string> series, 
            SortedSet<string> publishers, 
            StringBuilder exportText)
        {
            exportText.Append('"' + Escape(title) + '"' + ',');
            exportText.Append('"' + storyNumber + '"' + ',');
            exportText.Append('"' + EnumHelper<StoryType>.GetName(storyType) + '"' + ',');
            exportText.Append('"' + EnumHelper<BookType>.GetName(bookType) + '"' + ',');

            exportText.Append('"' + String.Join(",", EscapeList(characters)) + '"' + ',');
            exportText.Append('"' + String.Join(",", ArtistAndShortType(artists)) + '"' + ',');

            exportText.Append('"' + bookIssue.ToString() + '"' + ',');
            exportText.Append('"' + Escape(bookIssueTitle) + '"' + ',');
            exportText.Append('"' + language + '"' + ',');

            exportText.Append('"' + String.Join(",", EscapeList(series)) + '"' + ',');
            exportText.Append('"' + String.Join(",", EscapeList(publishers)) + '"' + ',');

            exportText.Append('"' + bookYear.ToString() + '"' + ',');
            exportText.Append('"' + purchaseDate.ToString("MM/dd/yyyy") + '"' + ',');
            exportText.Append('"' + Escape(notes??"") + '"' + ',');
            exportText.Append('"' + (deleted == Active.active ? "" : "del") + '"' + ',');
            exportText.AppendLine();
        }
    }
}
