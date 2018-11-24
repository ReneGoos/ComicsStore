namespace ComicsStore.MiddleWare.Models.Output
{
    public class BasicBookOutputModel
    {
        public int BookId { get; set; }

        public BookOnlyOutputModel Book { get; set; }
    }
}