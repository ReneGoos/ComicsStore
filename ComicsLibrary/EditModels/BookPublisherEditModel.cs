namespace ComicsLibrary.EditModels
{
    public class BookPublisherEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _bookId;
        private int? _publisherId;

        public int? BookId { get => _bookId; set => SetIfValue(ref _bookId, value); }
        public int? PublisherId { get => _publisherId; set => SetIfValue(ref _publisherId, value); }
        public PublisherOnlyEditModel Publisher { get; set; }

        public int? MainId { get => BookId; set => BookId = value; }
        public int? LinkedId { get => PublisherId; set => PublisherId = value; }
        public TableEditModel ChildItem { get => Publisher; set => Publisher = value as PublisherOnlyEditModel; }
    }
}