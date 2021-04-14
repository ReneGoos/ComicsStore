using System.Collections.Generic;
using System.Linq;

namespace ComicsLibrary.EditModels
{
    public class BookEditModel : TableEditModel
    {
        private ICollection<BookPublisherEditModel> _bookPublishers;
        private ICollection<BookSeriesEditModel> _bookSeries;
        private ICollection<StoryBookEditModel> _storyBooks;
        private string _bookType;
        private string _active;
        private int _firstYear;
        private int? _thisYear;
        private string _firstPrint;

        public BookEditModel() : base()
        {
            BookPublisher = new List<BookPublisherEditModel>();
            BookSeries = new List<BookSeriesEditModel>();
            StoryBook = new List<StoryBookEditModel>();
        }

        public string BookType { get => _bookType; set { Set(ref _bookType, value); } }
        public string Active { get => _active; set { Set(ref _active, value); } }
        public int FirstYear { get => _firstYear; set { Set(ref _firstYear, value); } }
        public int? ThisYear { get => _thisYear; set { Set(ref _thisYear, value); } }
        public string FirstPrint { get => _firstPrint; set { Set(ref _firstPrint, value); } }

        public ICollection<BookPublisherEditModel> BookPublisher { get => _bookPublishers; set { Set(ref _bookPublishers, value); } }
        public ICollection<BookSeriesEditModel> BookSeries { get => _bookSeries; set { Set(ref _bookSeries, value); } }
        public ICollection<StoryBookEditModel> StoryBook { get => _storyBooks; set { Set(ref _storyBooks, value); } }

        public void AddBookPublisher(ICollection<BookPublisherEditModel> bookPublishers, int? publisherId)
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

        public void AddBookStory(ICollection<StoryBookEditModel> bookStories, int? storyId)
        {
            if (storyId.HasValue)
            {
                if (!StoryBook.Any(s => s.StoryId == storyId.Value))
                {
                    if (!bookStories.Any(s => s.StoryId == storyId.Value))
                    {
                        bookStories.Add(new StoryBookEditModel
                        {
                            StoryId = storyId,
                            BookId = Id
                        });
                    }

                    StoryBook = bookStories;
                }
            }
        }

        public void AddBookSeries(ICollection<BookSeriesEditModel> bookSeries, int? seriesId)
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
    }
}
