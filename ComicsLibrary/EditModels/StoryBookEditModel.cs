namespace ComicsLibrary.EditModels
{
    public class StoryBookEditModel : CrossEditModel
    {
        private int _bookId;
        private int _storyId;

        public int BookId { get => _bookId; set { _bookId = value; RaisePropertyChanged(); } }
        public int StoryId { get => _storyId; set { _storyId = value; RaisePropertyChanged(); } }
    }
}