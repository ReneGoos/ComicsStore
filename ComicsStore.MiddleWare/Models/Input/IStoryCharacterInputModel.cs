namespace ComicsStore.MiddleWare.Models.Input
{
    public interface IStoryCharacterInputModel : IBasicCrossInputModel
    {
        int CharacterId { get; set; }
        int StoryId { get; set; }
    }
}