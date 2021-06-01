namespace ComicsStore.MiddleWare.Models.Output
{
    public interface ICharacterStoryOutputModel
    {
        int CharacterId { get; set; }
        int StoryId { get; set; }
    }
}