using ComicsLibrary.Core;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ComicsLibrary.EditModels
{
    public class BookEditModel : TableEditModel
    {
        private ObservableChangedCollection<BookPublisherEditModel> _bookPublishers;
        private ObservableChangedCollection<BookSeriesEditModel> _bookSeries;
        private ObservableChangedCollection<StoryBookEditModel> _storyBooks;
        private string _bookType;
        private string _active;
        private int _firstYear;
        private int? _thisYear;
        private string _firstPrint;

        public BookEditModel() : base()
        {
            BookPublisher = new ObservableChangedCollection<BookPublisherEditModel>();
            BookSeries = new ObservableChangedCollection<BookSeriesEditModel>();
            StoryBook = new ObservableChangedCollection<StoryBookEditModel>();
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
        public ObservableChangedCollection<StoryBookEditModel> StoryBook { get => _storyBooks; set => Set(ref _storyBooks, value); }

        public void AddBookPublisher(ObservableChangedCollection<BookPublisherEditModel> bookPublishers, int? publisherId)
        {
            if (publisherId.HasValue)
            {
                if (!BookPublisher.Any(s => s.PublisherId == publisherId.Value))
                {
                    if (!bookPublishers.Any(s => s.PublisherId == publisherId.Value))
                    {
                        bookPublishers.Add(new BookPublisherEditModel
                        {
                            PublisherId = publisherId,
                            BookId = Id
                        });
                    };

                    BookPublisher = bookPublishers;
                }
            }
        }

        public void AddStoryBook(ObservableChangedCollection<StoryBookEditModel> storyBooks, int? storyId)
        {
            if (storyId.HasValue)
            {
                if (!StoryBook.Any(s => s.StoryId == storyId.Value))
                {
                    if (!storyBooks.Any(s => s.StoryId == storyId.Value))
                    {
                        storyBooks.Add(new StoryBookEditModel
                        {
                            StoryId = storyId,
                            BookId = Id
                        });
                    }

                    StoryBook = storyBooks;
                }
            }
        }

        public void AddBookSeries(ObservableChangedCollection<BookSeriesEditModel> bookSeries, int? seriesId)
        {
            if (seriesId.HasValue)
            {
                if (!BookSeries.Any(s => s.SeriesId == seriesId.Value))
                {
                    if (!bookSeries.Any(s => s.SeriesId == seriesId.Value))
                    {
                        bookSeries.Add(new BookSeriesEditModel
                        {
                            SeriesId = seriesId,
                            BookId = Id
                        });
                    }

                    BookSeries = bookSeries;
                }
            }
        }

        public ObservableChangedCollection<StoryBookEditModel> GetStoryBooks()
        {
            return new ObservableChangedCollection<StoryBookEditModel>(StoryBook.ToList().ConvertAll(s => new StoryBookEditModel
            {
                BookId = s.BookId,
                StoryId = s.StoryId
            }));
        }

        public ObservableChangedCollection<BookPublisherEditModel> GetBookPublishers()
        {
            return new ObservableChangedCollection<BookPublisherEditModel>(BookPublisher.ToList().ConvertAll(s => new BookPublisherEditModel
            {
                BookId = s.BookId,
                PublisherId = s.PublisherId
            }));
        }

        public ObservableChangedCollection<BookSeriesEditModel> GetBookSeries()
        {
            return new ObservableChangedCollection<BookSeriesEditModel>(BookSeries.ToList().ConvertAll(s => new BookSeriesEditModel
            {
                BookId = s.BookId,
                SeriesId = s.SeriesId,
                Issue = s.Issue,
                SeriesOrder = s.SeriesOrder
            }));
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
