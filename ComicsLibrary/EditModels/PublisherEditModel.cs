using ComicsLibrary.Core;
using ComicsLibrary.Extensions;

namespace ComicsLibrary.EditModels
{
    public class PublisherEditModel : TableEditModel
    {
        private ObservableChangedCollection<PublisherBookEditModel> _bookPublishers;

        public PublisherEditModel() : base()
        {
            BookPublisher = new ObservableChangedCollection<PublisherBookEditModel>();
        }

        public ObservableChangedCollection<PublisherBookEditModel> BookPublisher { get => _bookPublishers; set => Set(ref _bookPublishers, value); }

        public void AddBookPublisher(int? bookId, int? oldBookId)
        {

            BookPublisher.HandleItem(Id, bookId, oldBookId);
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
