using ComicsStore.Data.Common;
using ComicsStore.Data.Model.Search;
using ComicsStore.Data.Repositories.Interfaces;
using ComicsStore.MiddleWare.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Reports
{
    public static class Reports
    {
        public async static Task<string> DataExportAsync(IExportBooksRepository repository, StorySeriesSearch searchModel)
        {
            var columnNames = @"""Title"",""Story number"",""Type"",""BookType"",""Character"",""Artist"",""Issue"",""Issue title"",""Language"",""Series"",""Publisher"",""Year"",""Purchase Date"",""Notes"",""Deleted""";

            Active deleted = default;
            string title = null;
            string storyNumber = null;
            StoryType storyType = default;
            BookType bookType = default;
            string bookIssue = null;
            string bookIssueTitle = null;
            string language = null;
            string notes = null;

            var storyId = -1;
            var bookId = -1;
            var seriesId = -1;
            var bookYear = -1;

            DateTime purchaseDate = default;

            var characters = new SortedSet<string>();
            var artists = new SortedList<string, ArtistType>();
            var series = new SortedSet<string>();
            var publishers = new SortedSet<string>();

            var exportText = new StringBuilder();

            _ = exportText.AppendLine(columnNames);

            //rsCodes = CodesDataset()
            //SqlDataReader rsStories = BooksDataset(pdbAll, pboolBooks, pboolPeriodicals, pboolDeleted, false);
            foreach (var stories in await repository.GetAsync(searchModel))
            {
                if (storyId == stories.StoryId && bookId == stories.BookId && seriesId == stories.SeriesId)
                {
                    _ = characters.Add(stories.Character);
                    artists[stories.Artist] = stories.ArtistType;
                    bookIssue = stories.Issue;
                    bookIssueTitle = stories.IssueTitle;
                    language = stories.Language;
                    _ = series.Add(stories.Series);
                    _ = publishers.Add(stories.Publisher);
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
                    storyType = stories.StoryType;
                    bookType = stories.BookType;
                    _ = characters.Add(stories.Character);
                    artists[stories.Artist] = stories.ArtistType;
                    bookIssue = stories.Issue;
                    bookIssueTitle = stories.IssueTitle;
                    language = stories.Language.Length == 0 ? "nl" : stories.Language;
                    _ = series.Add(stories.Series);
                    _ = publishers.Add(stories.Publisher);
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
            {
                return "";
            }

            return input.Replace('"', '\'');
        }

        private static ICollection<string> ArtistAndShortType(SortedList<string, ArtistType> artists)
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

        private static ICollection<string> EscapeList(SortedSet<string> input)
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
            SortedList<string, ArtistType> artists,
            SortedSet<string> series,
            SortedSet<string> publishers,
            StringBuilder exportText)
        {
            _ = exportText.Append('"' + Escape(title) + '"' + ',');
            _ = exportText.Append('"' + storyNumber + '"' + ',');
            _ = exportText.Append('"' + EnumHelper<StoryType>.GetName(storyType) + '"' + ',');
            _ = exportText.Append('"' + EnumHelper<BookType>.GetName(bookType) + '"' + ',');

            _ = exportText.Append('"' + String.Join(",", EscapeList(characters)) + '"' + ',');
            _ = exportText.Append('"' + String.Join(",", ArtistAndShortType(artists)) + '"' + ',');

            _ = exportText.Append('"' + bookIssue?.ToString() + '"' + ',');
            _ = exportText.Append('"' + Escape(bookIssueTitle) + '"' + ',');
            _ = exportText.Append('"' + language + '"' + ',');

            _ = exportText.Append('"' + String.Join(",", EscapeList(series)) + '"' + ',');
            _ = exportText.Append('"' + String.Join(",", EscapeList(publishers)) + '"' + ',');

            _ = exportText.Append('"' + bookYear.ToString() + '"' + ',');
            _ = exportText.Append('"' + purchaseDate.ToString("MM/dd/yyyy") + '"' + ',');
            _ = exportText.Append('"' + Escape(notes ?? "") + '"' + ',');
            _ = exportText.Append('"' + (deleted == Active.active ? "" : "del") + '"' + ',');
            _ = exportText.AppendLine();
        }
    }
}
