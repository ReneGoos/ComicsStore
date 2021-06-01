using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input
{
    public interface IBookInputModel : IBasicInputModel
    {
        string Active { get; set; }
        ICollection<BookPublisherInputModel> BookPublisher { get; set; }
        ICollection<BookSeriesInputModel> BookSeries { get; set; }
        string BookType { get; set; }
        string FirstPrint { get; set; }
        int FirstYear { get; set; }
        ICollection<StoryBookInputModel> StoryBook { get; set; }
        int? ThisYear { get; set; }
    }
}