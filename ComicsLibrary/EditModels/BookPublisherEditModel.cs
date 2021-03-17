namespace ComicsLibrary.EditModels
{
    public class BookPublisherEditModel : CrossEditModel
    {
        private int _bookId;
        private int _publisherId;

        public int BookId { get => _bookId; set { _bookId = value; RaisePropertyChanged(); } }
        public int PublisherId { get => _publisherId; set { _publisherId = value; RaisePropertyChanged(); } }
    }
}