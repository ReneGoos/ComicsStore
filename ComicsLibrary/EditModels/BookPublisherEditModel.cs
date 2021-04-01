namespace ComicsLibrary.EditModels
{
    public class BookPublisherEditModel : CrossEditModel
    {
        private int _bookId;
        private int _publisherId;

        public int BookId { get => _bookId; set { Set(ref _bookId, value); } }
        public int PublisherId { get => _publisherId; set { Set(ref _publisherId, value); } }
    }
}