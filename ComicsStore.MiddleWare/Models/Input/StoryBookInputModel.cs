namespace ComicsStore.MiddleWare.Models.Input
{
    public class StoryBookInputModel : BasicCrossInputModel, IStoryBookInputModel
    {
        public int StoryId { get; set; }
        public int BookId { get; set; }
    }
}