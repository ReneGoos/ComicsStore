namespace ComicsStore.MiddleWare.Models.Input
{
    public class StoryInputPatchModel : BasicInputModel, IStoryInputPatchModel
    {
        public string StoryType { get; set; }
        public decimal? StoryNumber { get; set; }
        public double? Pages { get; set; }
        public string ExtraInfo { get; set; }

        public int? CodeId { get; set; }
    }
}
