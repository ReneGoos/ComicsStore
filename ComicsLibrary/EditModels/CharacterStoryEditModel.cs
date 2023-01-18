namespace ComicsLibrary.EditModels
{
    public class CharacterStoryEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _characterId;
        private int? _storyId;

        public int? CharacterId { get => _characterId; set => SetIfValue(ref _characterId, value); }
        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }
        public StoryOnlyEditModel Story { get; set; }

        public int? MainId { get => CharacterId; set => CharacterId = value; }
        public int? LinkedId { get => StoryId; set => StoryId = value; }
        public TableEditModel ChildItem { get => Story; set => Story = value as StoryOnlyEditModel; }
    }
}