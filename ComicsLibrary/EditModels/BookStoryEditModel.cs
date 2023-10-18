namespace ComicsLibrary.EditModels
{
    public class BookStoryEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _bookId;
        private int? _storyId;
        private StoryOnlyEditModel _story;

        public int? BookId { get => _bookId; set => SetIfValue(ref _bookId, value); }
        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }
        public StoryOnlyEditModel Story { get => _story; set => SetIfValue(ref _story, value); }

        public int? MainId { get => BookId; set => BookId = value; }
        public int? LinkedId { get => StoryId; set => StoryId = value; }
        public TableEditModel ChildItem { get => Story; set => Story = value as StoryOnlyEditModel; }
    }
}