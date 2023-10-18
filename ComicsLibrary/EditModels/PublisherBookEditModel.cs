namespace ComicsLibrary.EditModels
{
    public class PublisherBookEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _bookId;
        private int? _publisherId;
        private BookOnlyEditModel _book;

        public int? BookId { get => _bookId; set => SetIfValue(ref _bookId, value); }
        public int? PublisherId { get => _publisherId; set => SetIfValue(ref _publisherId, value); }
        public BookOnlyEditModel Book { get => _book; set => SetIfValue(ref _book, value); }

        public int? MainId { get => PublisherId; set => PublisherId = value; }
        public int? LinkedId { get => BookId; set => BookId = value; }
        public TableEditModel ChildItem { get => Book; set => Book = value as BookOnlyEditModel; }
    }
}