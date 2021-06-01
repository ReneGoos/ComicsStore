namespace ComicsStore.MiddleWare.Models.Input
{
    public interface IStoryBookInputModel : IBasicCrossInputModel
    {
        int BookId { get; set; }
        int StoryId { get; set; }
    }
}