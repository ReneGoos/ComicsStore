namespace ComicsStore.MiddleWare.Models.Output
{
    public class CodeStoryOutputModel : ICodeStoryOutputModel
    {
        public int StoryId { get; set; }

        public StoryOnlyOutputModel Story { get; set; }
    }
}