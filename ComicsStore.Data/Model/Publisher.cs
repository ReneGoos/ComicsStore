using System.Collections.Generic;

namespace ComicsStore.Data.Model
{
    public class Publisher : MainTable
    {
        public Publisher()
            : base()
        {
            BookPublisher = new HashSet<BookPublisher>();
            //BronPublisher = new HashSet<BronPublisher>();
        }

        public ICollection<BookPublisher> BookPublisher { get; set; }
        //public ICollection<BronPublisher> BronPublisher { get; set; }
    }
}