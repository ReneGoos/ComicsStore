namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryCharacterOutputModel : CharacterOnlyOutputModel
    {
        public int CharacterId { get; set; }
        public int StoryId { get; set; }
    }
}
