using System;

namespace ComicsStore.Data.Model
{
    public class BookPublisher
    {
        public BookPublisher()
        {
            CreationDate = DateTime.Now;
            DateUpdate = DateTime.Now;
        }

        public int BookId { get; set; }
        public int PublisherId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateUpdate { get; set; }

        public Book Book { get; set; }
        public Publisher Publisher { get; set; }
    }
}