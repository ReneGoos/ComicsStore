using ComicsLibrary.EditModels.Interfaces;

namespace ComicsLibrary.EditModels
{
    public class BookPublisherEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _bookId;
        private int? _publisherId;
        private PublisherOnlyEditModel _publisher;

        public int? BookId { get => _bookId; set => SetIfValue(ref _bookId, value); }
        public int? PublisherId { get => _publisherId; set => SetIfValue(ref _publisherId, value); }
        public PublisherOnlyEditModel Publisher { get => _publisher; set => SetIfValue(ref _publisher, value); }

        public int? MainId { get => BookId; set => BookId = value; }
        public int? LinkedId { get => PublisherId; set => PublisherId = value; }
        public TableEditModel ChildItem { get => Publisher; set => Publisher = value as PublisherOnlyEditModel; }
    }
}