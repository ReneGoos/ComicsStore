namespace ComicsLibrary.EditModels
{
    public interface ICrossEditModel : IBasicEditModel
    {
        int? LinkedId { get; set; }
        int? MainId { get; set; }
        TableEditModel ChildItem { get; set; }
    }
}