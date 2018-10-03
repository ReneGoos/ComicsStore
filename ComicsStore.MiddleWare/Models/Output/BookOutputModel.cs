using ComicsStore.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class BookOutputModel : BasicOutputModel
    {
        public BookType BookType { get; set; }
        public Active Active { get; set; }
        public int FirstYear { get; set; }
        public int? ThisYear { get; set; }
        public int? FirstPrint { get; set; }

        public ICollection<BookSeriesOutputModel> BookSeries { get; set; }
        public ICollection<PublisherOutputModel> BookPublisher { get; set; }
        public ICollection<StoryOutputModel> BookStory { get; set; }
    }
}
