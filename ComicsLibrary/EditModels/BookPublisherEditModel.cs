namespace ComicsLibrary.EditModels
{
    public class BookPublisherEditModel : CrossEditModel
    {
        private int? _bookId;
        private int? _publisherId;

        public int? BookId { get => _bookId; set => SetIfValue(ref _bookId, value); }
        public int? PublisherId { get => _publisherId; set => SetIfValue(ref _publisherId, value); }

        public override int? MainId { get => BookId; set => BookId = value; }
        public override int? LinkedId { get => PublisherId; set => PublisherId = value; }
    }
}