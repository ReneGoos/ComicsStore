using ComicsLibrary.Core;
using System.Collections.Generic;
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

        public void AddBookPublisher(int? publisherId)
        {
            if (publisherId.HasValue)
            {
                if (!BookPublisher.Any(s => s.PublisherId == publisherId.Value))
                {
                    BookPublisher.Add(new BookPublisherEditModel
                    {
                        PublisherId = publisherId,
                        BookId = Id
                    });
                };
            }
        }

        public void AddStoryBook(int? storyId)
        {
            if (storyId.HasValue)
            {
                if (!StoryBook.Any(s => s.StoryId == storyId.Value))
                {
                    StoryBook.Add(new StoryBookEditModel
                    {
                        StoryId = storyId,
                        BookId = Id
                    });
                }
            }
        }

        public void AddBookSeries(int? seriesId)
        {
            if (seriesId.HasValue)
            {
                if (!BookSeries.Any(s => s.SeriesId == seriesId.Value))
                {
                    BookSeries.Add(new BookSeriesEditModel
                    {
                        SeriesId = seriesId,
                        BookId = Id
                    });
                }
            }
        }

        public List<StoryBookEditModel> GetStoryBooks()
        {
            return new List<StoryBookEditModel>(StoryBook.ToList().ConvertAll(s => new StoryBookEditModel
            {
                BookId = s.BookId,
                StoryId = s.StoryId
            }));
        }

        public List<BookPublisherEditModel> GetBookPublishers()
        {
            return new List<BookPublisherEditModel>(BookPublisher.ToList().ConvertAll(s => new BookPublisherEditModel
            {
                BookId = s.BookId,
                PublisherId = s.PublisherId
            }));
        }

        public List<BookSeriesEditModel> GetBookSeries()
        {
            return new List<BookSeriesEditModel>(BookSeries.ToList().ConvertAll(s => new BookSeriesEditModel
            {
                BookId = s.BookId,
                SeriesId = s.SeriesId,
                Issue = s.Issue,
                SeriesOrder = s.SeriesOrder
            }));
        }
    }
}
