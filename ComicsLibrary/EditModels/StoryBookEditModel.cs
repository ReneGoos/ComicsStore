namespace ComicsLibrary.EditModels
{
    public class StoryBookEditModel : CrossEditModel
    {
        private int? _bookId;
        private int? _storyId;

        public int? BookId { get => _bookId; set => SetIfValue(ref _bookId, value); }
        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }

        public override int? MainId { get => StoryId; set => StoryId = value; }
        public override int? LinkedId { get => BookId; set => BookId = value; }
    }
}