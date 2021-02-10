using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class BookOutputModel : BookOnlyOutputModel
    {
        public ICollection<BookSeriesOutputModel> BookSeries { get; set; }
        public ICollection<BookPublisherOutputModel> BookPublisher { get; set; }
        public ICollection<BookStoryOutputModel> StoryBook { get; set; }
    }
}
