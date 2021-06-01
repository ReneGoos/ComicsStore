namespace ComicsStore.MiddleWare.Models.Input
{
    public class StoryCharacterInputModel : BasicCrossInputModel, IStoryCharacterInputModel
    {
        public int StoryId { get; set; }
        public int CharacterId { get; set; }
    }
}
