using System.Collections.Generic;

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

    public class StoryBookComparer : IEqualityComparer<StoryBook>
    {
        public bool Equals(StoryBook x, StoryBook y)
        {
            return (x.BookId == y.BookId && x.StoryId == y.StoryId);
        }

        public int GetHashCode(StoryBook x)
        {
            return x.StoryId + x.BookId;
        }
    }
}