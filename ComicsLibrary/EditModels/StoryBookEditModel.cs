namespace ComicsLibrary.EditModels
{
    public class StoryBookEditModel : CrossEditModel
    {
        private int? _bookId;
        private int? _storyId;

        public int? BookId { get => _bookId; set => Set(ref _bookId, value); }
        public int? StoryId { get => _storyId; set => Set(ref _storyId, value); }
    }
}