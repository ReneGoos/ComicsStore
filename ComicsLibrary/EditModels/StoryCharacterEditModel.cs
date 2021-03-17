namespace ComicsLibrary.EditModels
{
    public class StoryCharacterEditModel : CrossEditModel
    {
        private int _characterId;
        private int _storyId;

        public int CharacterId { get => _characterId; set { _characterId = value; RaisePropertyChanged(); }}
        public int StoryId { get => _storyId; set { _storyId = value; RaisePropertyChanged(); }}
    }
}