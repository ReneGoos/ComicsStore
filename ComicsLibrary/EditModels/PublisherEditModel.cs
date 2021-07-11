using System.Collections.Generic;
using System.Linq;

namespace ComicsLibrary.EditModels
{
    public class PublisherEditModel : TableEditModel
    {
        private ICollection<BookPublisherEditModel> _bookPublishers;

        public PublisherEditModel() : base()
        {
            BookPublisher = new List<BookPublisherEditModel>();
        }

        public ICollection<BookPublisherEditModel> BookPublisher { get => _bookPublishers; set => Set(ref _bookPublishers, value); }

        public void AddBookPublisher(List<BookPublisherEditModel> bookPublishers, int? bookId)
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
                    };

                    BookPublisher = bookPublishers;
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
