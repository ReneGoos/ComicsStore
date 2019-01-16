namespace ComicsStore.MiddleWare.Models.Output
{
    public class BasicBookOutputModel : BasicCrossOutputModel
    {
        public int BookId { get; set; }

        public BookOnlyOutputModel Book { get; set; }
    }
}