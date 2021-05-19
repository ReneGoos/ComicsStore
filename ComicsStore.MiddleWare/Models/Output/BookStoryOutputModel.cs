namespace ComicsStore.MiddleWare.Models.Output
{
    public class BookStoryOutputModel : StoryOnlyOutputModel
    {
        public int BookId { get; set; }
        public int StoryId { get; set; }
    }
}