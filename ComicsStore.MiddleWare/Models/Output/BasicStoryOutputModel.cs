namespace ComicsStore.MiddleWare.Models.Output
{
    public class BasicStoryOutputModel : BasicCrossOutputModel
    {
        public int StoryId { get; set; }

        public StoryOnlyOutputModel Story { get; set; }
    }
}