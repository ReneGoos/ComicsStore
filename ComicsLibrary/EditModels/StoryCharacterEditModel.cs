namespace ComicsLibrary.EditModels
{
    public class StoryCharacterEditModel : CrossEditModel
    {
        private int? _characterId;
        private int? _storyId;

        public int? CharacterId { get => _characterId; set => SetIfValue(ref _characterId, value); }
        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }

        public override int? MainId { get => StoryId; set => StoryId = value; }
        public override int? LinkedId { get => CharacterId; set => CharacterId = value; }
    }
}