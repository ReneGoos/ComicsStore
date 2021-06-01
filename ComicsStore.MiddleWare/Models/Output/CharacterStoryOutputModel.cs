namespace ComicsStore.MiddleWare.Models.Output
{
    public class CharacterStoryOutputModel : StoryOnlyOutputModel, ICharacterStoryOutputModel
    {
        public int CharacterId { get; set; }
        public int StoryId { get; set; }
    }
}
