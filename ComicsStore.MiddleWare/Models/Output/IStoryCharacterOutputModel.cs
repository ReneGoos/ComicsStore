namespace ComicsStore.MiddleWare.Models.Output
{
    public interface IStoryCharacterOutputModel
    {
        int CharacterId { get; set; }
        int StoryId { get; set; }
    }
}