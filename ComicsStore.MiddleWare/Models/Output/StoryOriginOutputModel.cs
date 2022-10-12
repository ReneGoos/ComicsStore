namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryOriginOutputModel : StoryOnlyOutputModel, IStoryOriginOutputModel
    {
        public int StoryId { get; set; }
    }
}