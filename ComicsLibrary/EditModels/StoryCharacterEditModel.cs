using ComicsStore.Data.Model;

namespace ComicsLibrary.EditModels
{
    public class StoryCharacterEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _characterId;
        private int? _storyId;

        public int? CharacterId { get => _characterId; set => SetIfValue(ref _characterId, value); }
        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }
        public CharacterOnlyEditModel Character { get; set; }

        public int? MainId { get => StoryId; set => StoryId = value; }
        public int? LinkedId { get => CharacterId; set => CharacterId = value; }
        public TableEditModel ChildItem { get => Character; set => Character = value as CharacterOnlyEditModel; }
    }
}