using ComicsLibrary.Core;
using System.Collections.Generic;
using System.Linq;

namespace ComicsLibrary.EditModels
{
    public class PublisherEditModel : TableEditModel
    {
        private ObservableChangedCollection<BookPublisherEditModel> _bookPublishers;

        public PublisherEditModel() : base()
        {
            BookPublisher = new ObservableChangedCollection<BookPublisherEditModel>();
        }

        public ObservableChangedCollection<BookPublisherEditModel> BookPublisher { get => _bookPublishers; set => Set(ref _bookPublishers, value); }

        public void AddBookPublisher(int? bookId)
        {
            if (bookId.HasValue)
            {
                if (!BookPublisher.Any(s => s.BookId == bookId.Value))
                {
                    BookPublisher.Add(new BookPublisherEditModel
                    {
                        PublisherId = Id,
                        BookId = bookId
                    });
                }
            }
        }

        public List<BookPublisherEditModel> GetBookPublishers()
        {
            return new List<BookPublisherEditModel>(BookPublisher.ToList().ConvertAll(s => new BookPublisherEditModel
            {
                BookId = s.BookId,
                PublisherId = s.PublisherId
            }));
        }
    }
}
