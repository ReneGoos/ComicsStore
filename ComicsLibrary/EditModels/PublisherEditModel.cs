using ComicsLibrary.Core;
using ComicsLibrary.Extensions;

namespace ComicsLibrary.EditModels
{
    public class PublisherEditModel : PublisherOnlyEditModel
    {
        private ObservableChangedCollection<PublisherBookEditModel> _bookPublishers;

        public PublisherEditModel() : base()
        {
            BookPublisher = new ObservableChangedCollection<PublisherBookEditModel>();
        }

        public ObservableChangedCollection<PublisherBookEditModel> BookPublisher { get => _bookPublishers; set => Set(ref _bookPublishers, value); }

        public void HandleBook(int? oldBookId, BookOnlyEditModel book)
        {

            BookPublisher.HandleItem(Id, oldBookId, book);
        }

        public void ResetId()
        {
            Id = null;

            foreach (var book in BookPublisher)
            {
                book.PublisherId = null;
            }
        }
    }
}
