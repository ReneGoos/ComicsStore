using System;

namespace ComicsStore.Data.Model
{
    public class StoryBook : CrossTable
    {
        public StoryBook() : base()
        {
        }

        public int StoryId { get; set; }
        public int BookId { get; set; }

        public Story Story { get; set; }
        public Book Book { get; set; }
    }
}