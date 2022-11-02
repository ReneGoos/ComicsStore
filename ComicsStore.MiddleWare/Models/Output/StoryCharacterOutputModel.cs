namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryCharacterOutputModel : IStoryCharacterOutputModel
    {
        public int CharacterId { get; set; }
        public int StoryId { get; set; }

        public CharacterOnlyOutputModel Character { get; set; }
    }
}
