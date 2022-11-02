namespace ComicsStore.MiddleWare.Models.Output
{
    public class CharacterStoryOutputModel : ICharacterStoryOutputModel
    {
        public int CharacterId { get; set; }
        public int StoryId { get; set; }

        public StoryOnlyOutputModel Story { get; set; }
    }
}
