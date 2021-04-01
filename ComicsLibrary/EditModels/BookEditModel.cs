using System.Collections.Generic;

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

        public string BookType { get => _bookType; set { Set(ref _bookType, value); } }
        public string Active { get => _active; set { Set(ref _active, value); } }
        public int FirstYear { get => _firstYear; set { Set(ref _firstYear, value); } }
        public int? ThisYear { get => _thisYear; set { Set(ref _thisYear, value); } }
        public string FirstPrint { get => _firstPrint; set { Set(ref _firstPrint, value); } }

        public ICollection<BookPublisherEditModel> BookPublisher { get => _bookPublishers; set { Set(ref _bookPublishers, value); } }
        public ICollection<BookSeriesEditModel> BookSeries { get => _bookSeries; set { Set(ref _bookSeries, value); } }
        public ICollection<StoryBookEditModel> StoryBook { get => _storyBooks; set { Set(ref _storyBooks, value); } }
    }
}
