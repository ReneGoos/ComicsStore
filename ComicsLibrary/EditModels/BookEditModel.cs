using ComicsLibrary.Core;
using System.ComponentModel.DataAnnotations;
using ComicsLibrary.Extensions;

namespace ComicsLibrary.EditModels
{
    public class BookEditModel : TableEditModel
    {
        private ObservableChangedCollection<BookPublisherEditModel> _bookPublishers;
        private ObservableChangedCollection<BookSeriesEditModel> _bookSeries;
        private ObservableChangedCollection<BookStoryEditModel> _bookStories;
        private string _bookType;
        private string _active;
        private int _firstYear;
        private int? _thisYear;
        private string _firstPrint;

        public BookEditModel() : base()
        {
            BookPublisher = new ObservableChangedCollection<BookPublisherEditModel>();
            BookSeries = new ObservableChangedCollection<BookSeriesEditModel>();
            StoryBook = new ObservableChangedCollection<BookStoryEditModel>();
        }

        [Required]
        public string BookType { get => _bookType; set => Set(ref _bookType, value); }
        [Required]
        public string Active { get => _active; set => Set(ref _active, value); }
        [Required]
        public int FirstYear { get => _firstYear; set => Set(ref _firstYear, value); }
        public int? ThisYear { get => _thisYear; set => Set(ref _thisYear, value); }
        [Required]
        public string FirstPrint { get => _firstPrint; set => Set(ref _firstPrint, value); }

        public ObservableChangedCollection<BookPublisherEditModel> BookPublisher { get => _bookPublishers; set => Set(ref _bookPublishers, value); }
        public ObservableChangedCollection<BookSeriesEditModel> BookSeries { get => _bookSeries; set => Set(ref _bookSeries, value); }
        public ObservableChangedCollection<BookStoryEditModel> StoryBook { get => _bookStories; set => Set(ref _bookStories, value); }

        public void AddBookPublisher(int? publisherId, int? oldPublisherId)
        {
            BookPublisher.HandleItem(Id, publisherId, oldPublisherId);
        }

        public void AddBookStory(int? storyId, int? oldStoryId)
        {
            StoryBook.HandleItem(Id, storyId, oldStoryId);
        }

        public void AddBookSeries(int? seriesId, int? oldSeriesId)
        {
            BookSeries.HandleItem(Id, seriesId, oldSeriesId);
        }

        public override void ResetId()
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
