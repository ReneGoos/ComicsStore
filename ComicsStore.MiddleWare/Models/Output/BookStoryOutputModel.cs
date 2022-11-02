namespace ComicsStore.MiddleWare.Models.Output
{
    public class BookStoryOutputModel : IBookStoryOutputModel
    {
        public int BookId { get; set; }
        public int StoryId { get; set; }

        public StoryOnlyOutputModel Story { get; set; }
    }
}