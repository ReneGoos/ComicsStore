namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryBookOutputModel : BookOnlyOutputModel
    {
        public int BookId { get; set; }
        public int StoryId { get; set; }
    }
}
