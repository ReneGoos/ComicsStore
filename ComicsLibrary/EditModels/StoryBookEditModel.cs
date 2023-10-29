using ComicsLibrary.EditModels.Interfaces;

namespace ComicsLibrary.EditModels
{
    public class StoryBookEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _bookId;
        private int? _storyId;
        private BookOnlyEditModel _book;

        public int? BookId { get => _bookId; set => SetIfValue(ref _bookId, value); }
        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }
        public BookOnlyEditModel Book { get => _book; set => SetIfValue(ref _book, value); }

        public int? MainId { get => StoryId; set => StoryId = value; }
        public int? LinkedId { get => BookId; set => BookId = value; }
        public TableEditModel ChildItem { get => Book; set => Book = value as BookOnlyEditModel; }
    }
}