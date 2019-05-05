namespace ComicsStore.Data.Model
{
    public class BookPublisher : CrossTable
    {
        public BookPublisher()
            : base()
        {
        }

        public int BookId { get; set; }
        public int PublisherId { get; set; }

        public Book Book { get; set; }
        public Publisher Publisher { get; set; }
    }
}