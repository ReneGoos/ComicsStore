using System;

namespace ComicsStore.Data.Model
{
    public class StoryBook
    {
        public StoryBook()
        {
            CreationDate = DateTime.Now;
            DateUpdate = DateTime.Now;
        }

        public int StoryId { get; set; }
        public int BookId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateUpdate { get; set; }

        public Story Story { get; set; }
        public Book Book { get; set; }
    }
}