namespace ComicsStore.MiddleWare.Models.Input
{
    public class StoryCharacterInputModel : BasicCrossInputModel
    {
        public int StoryId { get; set; }
        public int CharacterId { get; set; }
    }
}
