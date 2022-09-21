using ComicsLibrary.Core;
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

        public void AddBookPublisher(ObservableChangedCollection<BookPublisherEditModel> bookPublishers, int? bookId)
        {
            if (bookId.HasValue)
            {
                if (!BookPublisher.Any(s => s.BookId == bookId.Value))
                {
                    if (!bookPublishers.Any(s => s.BookId == bookId.Value))
                    {
                        bookPublishers.Add(new BookPublisherEditModel
                        {
                            PublisherId = Id,
                            BookId = bookId
                        });
                    }
                }

                BookPublisher = bookPublishers;
            }
        }

        public ObservableChangedCollection<BookPublisherEditModel> GetBookPublishers()
        {
            return new ObservableChangedCollection<BookPublisherEditModel>(BookPublisher.ToList().ConvertAll(s => new BookPublisherEditModel
            {
                BookId = s.BookId,
                PublisherId = s.PublisherId
            }));
        }

        public override void ResetId()
        {
            Id = null;

            foreach (var book in BookPublisher)
            {
                book.PublisherId = null;
            }
        }
    }
}
