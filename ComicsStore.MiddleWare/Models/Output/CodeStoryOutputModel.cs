namespace ComicsStore.MiddleWare.Models.Output
{
    public class CodeStoryOutputModel : StoryOnlyOutputModel, ICodeStoryOutputModel
    {
        public int StoryId { get; set; }
    }
}