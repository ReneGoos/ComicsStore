﻿using ComicsStore.Data.Common;
using ComicsStore.Data.Model.Output;
using ComicsStore.MiddleWare.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicsStore.MiddleWare.Reports
{
    public static class Reports
    {
        public static string DataExport(List<ExportBook> stories)
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
            foreach (var story in stories)
            {
                if (storyId == story.StoryId && bookId == story.BookId && seriesId == story.SeriesId)
                {
                    _ = characters.Add(story.Character);
                    artists[story.Artist] = story.ArtistType;
                    bookIssue = story.Issue;
                    bookIssueTitle = story.IssueTitle;
                    language = story.Language;
                    _ = series.Add(story.Series);
                    _ = publishers.Add(story.Publisher);
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

                    characters = [];
                    artists = [];
                    series = [];
                    publishers = [];

                    storyId = story.StoryId;
                    bookId = story.BookId;
                    seriesId = story.SeriesId;
                    title = story.Title + (story.OriginalTitle is null || story.OriginalTitle.Equals(story.Title) ? null : " (" + story.OriginalTitle + ")");
                    storyNumber = !story.StoryNumber.HasValue ? story.ExtraInfo : story.StoryNumber.Value.ToString();
                    storyType = story.StoryType;
                    bookType = story.BookType;
                    _ = characters.Add(story.Character);
                    artists[story.Artist] = story.ArtistType;
                    bookIssue = story.Issue;
                    bookIssueTitle = story.IssueTitle;
                    language = story.Language.Length == 0 ? "nl" : story.Language;
                    _ = series.Add(story.Series);
                    _ = publishers.Add(story.Publisher);
                    bookYear = !story.Year.HasValue ? -1 : story.Year.Value;
                    purchaseDate = story.PurchaseDate;
                    deleted = story.Deleted;
                    notes = story.Notes;
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
