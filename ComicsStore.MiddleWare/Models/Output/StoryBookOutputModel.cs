namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryBookOutputModel : IStoryBookOutputModel
    {
        public int BookId { get; set; }
        public int StoryId { get; set; }

        public BookOnlyOutputModel Book { get; set; }
    }
}
