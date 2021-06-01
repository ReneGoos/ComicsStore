using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class BookInputModel : BasicInputModel, IBookInputModel
    {
        public string BookType { get; set; }
        public string Active { get; set; }
        public int FirstYear { get; set; }
        public int? ThisYear { get; set; }
        public string FirstPrint { get; set; }

        public ICollection<BookSeriesInputModel> BookSeries { get; set; }
        public ICollection<BookPublisherInputModel> BookPublisher { get; set; }
        public ICollection<StoryBookInputModel> StoryBook { get; set; }
    }
}