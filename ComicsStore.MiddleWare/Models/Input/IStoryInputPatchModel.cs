namespace ComicsStore.MiddleWare.Models.Input
{
    public interface IStoryInputPatchModel : IBasicInputModel
    {
        int? CodeId { get; set; }
        string ExtraInfo { get; set; }
        double? Pages { get; set; }
        int? StoryNumber { get; set; }
        string StoryType { get; set; }
    }
}