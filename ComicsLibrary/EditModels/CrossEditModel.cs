namespace ComicsLibrary.EditModels
{
    public abstract class CrossEditModel : BasicEditModel
    {
        public abstract int? MainId { get; set; }
        public abstract int? LinkedId { get; set; }
    }
}
