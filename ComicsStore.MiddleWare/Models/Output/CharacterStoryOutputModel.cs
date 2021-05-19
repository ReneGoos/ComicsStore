namespace ComicsStore.MiddleWare.Models.Output
{
    public class CharacterStoryOutputModel : StoryOnlyOutputModel
    {
        public int CharacterId { get; set; }
        public int StoryId { get; set; }
    }
}
