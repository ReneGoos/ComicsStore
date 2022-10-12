namespace ComicsLibrary.EditModels
{
    public class CharacterStoryEditModel : StoryCharacterEditModel
    {
        public override int? MainId { get => CharacterId; set => CharacterId = value; }
        public override int? LinkedId { get => StoryId; set => StoryId = value; }
    }
}