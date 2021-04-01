namespace ComicsLibrary.EditModels
{
    public class StoryCharacterEditModel : CrossEditModel
    {
        private int _characterId;
        private int _storyId;

        public int CharacterId { get => _characterId; set { Set(ref _characterId, value); }}
        public int StoryId { get => _storyId; set { Set(ref _storyId, value); }}
    }
}