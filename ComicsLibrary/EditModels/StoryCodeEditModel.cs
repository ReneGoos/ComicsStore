namespace ComicsLibrary.EditModels
{
    public class StoryCodeEditModel : CrossEditModel
    {
        private int _storyId;
        private int _codeId;

        public int CodeId { get => _codeId; set { _codeId = value; RaisePropertyChanged(); } }
        public int StoryId { get => _storyId; set { _storyId = value; RaisePropertyChanged(); } }
    }
}