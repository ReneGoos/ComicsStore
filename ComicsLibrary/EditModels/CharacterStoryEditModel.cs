using ComicsLibrary.EditModels.Interfaces;

namespace ComicsLibrary.EditModels
{
    public class CharacterStoryEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _characterId;
        private int? _storyId;
        private StoryOnlyEditModel _story;

        public int? CharacterId { get => _characterId; set => SetIfValue(ref _characterId, value); }
        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }
        public StoryOnlyEditModel Story { get => _story; set => SetIfValue(ref _story, value); }

        public int? MainId { get => CharacterId; set => CharacterId = value; }
        public int? LinkedId { get => StoryId; set => StoryId = value; }
        public TableEditModel ChildItem { get => Story; set => Story = value as StoryOnlyEditModel; }
    }
}