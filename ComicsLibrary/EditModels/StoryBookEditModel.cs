using ComicsStore.Data.Model;

namespace ComicsLibrary.EditModels
{
    public class StoryBookEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _bookId;
        private int? _storyId;

        public int? BookId { get => _bookId; set => SetIfValue(ref _bookId, value); }
        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }
        public BookOnlyEditModel Book { get; set; }

        public int? MainId { get => StoryId; set => StoryId = value; }
        public int? LinkedId { get => BookId; set => BookId = value; }
        public TableEditModel ChildItem { get => Book; set => Book = value as BookOnlyEditModel; }
    }
}