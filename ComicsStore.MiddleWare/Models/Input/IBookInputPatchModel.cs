namespace ComicsStore.MiddleWare.Models.Input
{
    public interface IBookInputPatchModel : IBasicInputModel
    {
        string Active { get; set; }
        string BookType { get; set; }
        string FirstPrint { get; set; }
        int? FirstYear { get; set; }
        int? ThisYear { get; set; }
    }
}