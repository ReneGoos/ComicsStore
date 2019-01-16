using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Common;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Reports
{
    public static class Reports
    {
        public async static Task<string> DataExport(IExportMementoRepository repository, string sFilename, bool pboolBooks, bool pboolPeriodicals, bool pboolDeleted, DateTime pdtPurchaseDate = default)
        {
            string strNames = @"""Title"",""Story number"",""Type"",""BookType"",""Character"",""Artist"",""Issue"",""Issue title"",""Language"",""Series"",""Publisher"",""Year"",""Purchase Date"",""Notes"",""Deleted""";
            //"Title","Story number","Type","Character","Artist","Writer","Issue","Issue title","Language","Series","Publisher","Year","Purchase Date","Notes","Deleted"
            //unique key "Title","Story number","Type","Issue","Issue title"
            //list "Character","Artist","Writer","Series","Publisher"
            //others "Language","Year","Purchase Date","Notes","Deleted"

            StoryType storyType = default;
            BookType bookType = default;
            Active deleted = default;
            string strTitle = null, strStoryNumber = null, strType = null, strBookType = null, strIssue = null, strIssueTitle = null;
            string strLanguage = null, strNotes = null;
            int iYear = -1;

            DateTime dtPurchaseDate = default;

            var lstCharacter = new SortedSet<string>();
            var lstArtist = new SortedSet<string>();
            var lstSeries = new SortedSet<string>();
            var lstPublisher = new SortedSet<string>();
            var first = true;

            StringBuilder saRTF = new StringBuilder();

            saRTF.AppendLine(strNames);

            //rsCodes = CodesDataset()
            //SqlDataReader rsStories = BooksDataset(pdbAll, pboolBooks, pboolPeriodicals, pboolDeleted, false);
            foreach (var rsStories in await repository.GetAsync(new ExportMementoSearchModel { Active = pboolDeleted ? default : Active.active }))
            {
                if (!first)
                {
                    if ((strTitle.Equals(rsStories.Title.Replace('"', '\'')) &&
                        strStoryNumber.Equals(!rsStories.StoryNumber.HasValue ? rsStories.ExtraInfo : rsStories.StoryNumber.Value.ToString()) &&
                        storyType == (StoryType)rsStories.StoryType &&
                        bookType == (BookType)rsStories.BookType &&
                        strIssue.Equals(rsStories.Issue)) &&
                        strIssueTitle.Equals(rsStories.IssueTitle.Replace('"', '\'')))
                    {
                        lstCharacter.Add(rsStories.Character.Replace('"', '\''));
                        lstArtist.Add(rsStories.Artist.Replace('"', '\'') + ' ' + EnumHelper<ArtistType>.GetName((ArtistType)rsStories.ArtistType));
                        strIssue = rsStories.Issue;
                        strIssueTitle = rsStories.IssueTitle;
                        strLanguage = rsStories.Language;
                        lstSeries.Add(rsStories.Series.Replace('"', '\''));
                        lstPublisher.Add(rsStories.Publisher.Replace('"', '\''));
                    }
                    else
                        AddToOutput(deleted, strTitle, strStoryNumber, strType, strBookType, strIssue, strIssueTitle, strLanguage, strNotes, iYear, dtPurchaseDate, ref lstCharacter, ref lstArtist, ref lstSeries, ref lstPublisher, saRTF);
                }


                strTitle = rsStories.Title.Replace('"', '\'');
                strStoryNumber = !rsStories.StoryNumber.HasValue ? rsStories.ExtraInfo : rsStories.StoryNumber.Value.ToString();
                storyType = (StoryType)rsStories.StoryType;
                strType = EnumHelper<StoryType>.GetName(storyType);
                bookType = (BookType)rsStories.BookType;
                strBookType = EnumHelper<BookType>.GetName(bookType);
                lstCharacter.Add(rsStories.Character.Replace('"', '\''));
                lstArtist.Add(rsStories.Artist.Replace('"', '\'') + ' ' + EnumHelper<ArtistType>.GetName((ArtistType)rsStories.ArtistType));
                strIssue = rsStories.Issue;
                strIssueTitle = rsStories.IssueTitle.Replace('"', '\'');
                strLanguage = rsStories.Language.Length == 0 ? "nl" : rsStories.Language;
                lstSeries.Add(rsStories.Series.Replace('"', '\''));
                lstPublisher.Add(rsStories.Publisher.Replace('"', '\''));
                iYear = !rsStories.Year.HasValue ? -1 : rsStories.Year.Value;
                dtPurchaseDate = rsStories.PurchaseDate;
                deleted = rsStories.Deleted;
                strNotes = rsStories.Notes;

                first = false;
            }

            AddToOutput(deleted, strTitle, strStoryNumber, strType, strBookType, strIssue, strIssueTitle, strLanguage, strNotes, iYear, dtPurchaseDate, ref lstCharacter, ref lstArtist, ref lstSeries, ref lstPublisher, saRTF);

            //    .Visible = True
            //    mdiMain.stbStatus.Panels(1).Text = "Klaar"
            return saRTF.ToString();
        }

        private static void AddToOutput(Active deleted, string strTitle, string strStoryNumber, string strType, string strBookType, string strIssue, string strIssueTitle, string strLanguage, string strNotes, int iYear, DateTime dtPurchaseDate, ref SortedSet<string> lstCharacter, ref SortedSet<string> lstArtist, ref SortedSet<string> lstSeries, ref SortedSet<string> lstPublisher, StringBuilder saRTF)
        {
            saRTF.Append('"' + strTitle.Replace('"', '\'') + '"' + ',');
            saRTF.Append('"' + strStoryNumber + '"' + ',');
            saRTF.Append('"' + strType + '"' + ',');
            saRTF.Append('"' + strBookType + '"' + ',');

            saRTF.Append('"' + String.Join(",", lstCharacter) + '"' + ',');
            saRTF.Append('"' + String.Join(",", lstArtist) + '"' + ',');

            saRTF.Append('"' + strIssue.ToString() + '"' + ',');
            saRTF.Append('"' + strIssueTitle.Replace('"', '\'') + '"' + ',');
            saRTF.Append('"' + strLanguage + '"' + ',');

            saRTF.Append('"' + String.Join(",", lstSeries) + '"' + ',');
            saRTF.Append('"' + String.Join(",", lstPublisher) + '"' + ',');

            saRTF.Append('"' + iYear.ToString() + '"' + ',');
            saRTF.Append('"' + dtPurchaseDate.ToString("MM/dd/yyyy") + '"' + ',');
            saRTF.Append('"' + (strNotes == null ? "" : strNotes.Replace('"', '\'')) + '"' + ',');
            saRTF.Append('"' + (deleted == Active.active ? "" : "del") + '"' + ',');
            saRTF.AppendLine();

            lstCharacter = new SortedSet<string>();
            lstArtist = new SortedSet<string>();
            lstSeries = new SortedSet<string>();
            lstPublisher = new SortedSet<string>();
        }
    }
}
