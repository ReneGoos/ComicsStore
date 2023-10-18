namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryOriginOutputModel : IStoryOriginOutputModel
    {
        public int OriginStoryId { get; set; }
        public int StoryId { get; set; }

        public StoryOnlyOutputModel StoryFromOrigin { get; set; }
    }
}