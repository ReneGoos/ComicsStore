using ComicsLibrary.Core;
using System.ComponentModel.DataAnnotations;
using ComicsLibrary.Extensions;

namespace ComicsLibrary.EditModels
{
    public class BookEditModel : BookOnlyEditModel
    {
        private ObservableChangedCollection<BookPublisherEditModel> _bookPublishers;
        private ObservableChangedCollection<BookSeriesEditModel> _bookSeries;
        private ObservableChangedCollection<BookStoryEditModel> _bookStories;

        public BookEditModel() : base()
        {
            BookPublisher = new ObservableChangedCollection<BookPublisherEditModel>();
            BookSeries = new ObservableChangedCollection<BookSeriesEditModel>();
            StoryBook = new ObservableChangedCollection<BookStoryEditModel>();
        }

        public ObservableChangedCollection<BookPublisherEditModel> BookPublisher { get => _bookPublishers; set => Set(ref _bookPublishers, value); }
        public ObservableChangedCollection<BookSeriesEditModel> BookSeries { get => _bookSeries; set => Set(ref _bookSeries, value); }
        public ObservableChangedCollection<BookStoryEditModel> StoryBook { get => _bookStories; set => Set(ref _bookStories, value); }

        public void HandlePublisher(int? oldPublisherId, PublisherOnlyEditModel publisher)
        {
            BookPublisher.HandleItem(Id, oldPublisherId, publisher);
        }

        public void HandleStory(int? oldStoryId, StoryOnlyEditModel story)
        {
            StoryBook.HandleItem(Id, oldStoryId, story);
        }

        public void HandleSeries(int? oldSeriesId, SeriesOnlyEditModel series)
        {
            BookSeries.HandleItem(Id, oldSeriesId, series);
        }

        public void ResetId()
        {
            Id = null;

            foreach (var story in StoryBook)
            {
                story.BookId = null;
            }


            foreach (var publisher in BookPublisher)
            {
                publisher.BookId = null;
            }

            foreach (var series in BookSeries)
            {
                series.BookId = null;
            }
        }
    }
}
