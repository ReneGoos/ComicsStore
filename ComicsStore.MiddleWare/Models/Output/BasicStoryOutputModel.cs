namespace ComicsStore.MiddleWare.Models.Output
{
    public class BasicStoryOutputModel
    {
        public int StoryId { get; set; }

        public StoryOnlyOutputModel Story { get; set; }
    }
}