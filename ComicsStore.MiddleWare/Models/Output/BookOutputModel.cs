using ComicsStore.Data.Model;
using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class BookOutputModel : BasicOutputModel
    {
        public string BookType { get; set; }
        public string Active { get; set; }
        public int FirstYear { get; set; }
        public int? ThisYear { get; set; }
        public string FirstPrint { get; set; }

        public ICollection<BookSeriesOutputModel> BookSeries { get; set; }
        public ICollection<BookPublisherOutputModel> BookPublisher { get; set; }
        public ICollection<BasicStoryOutputModel> StoryBook { get; set; }
    }
}
