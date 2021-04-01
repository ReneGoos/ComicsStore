namespace ComicsLibrary.EditModels
{
    public class StoryCodeEditModel : CrossEditModel
    {
        private int _storyId;
        private int _codeId;

        public int CodeId { get => _codeId; set { Set(ref _codeId, value); } }
        public int StoryId { get => _storyId; set { Set(ref _storyId, value); } }
    }
}