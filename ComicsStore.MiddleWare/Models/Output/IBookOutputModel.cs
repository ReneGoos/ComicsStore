﻿using ComicsStore.Data.Common;
using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public interface IBookOutputModel : IBasicOutputModel
    {
        string BookType { get; set; }
        string Active { get; set; }
        int FirstYear { get; set; }
        int? ThisYear { get; set; }
        string FirstPrint { get; set; }
        string Signed { get; set; }
        string Checked { get; set; }
        string CoverType { get; set; }

        ICollection<BookPublisherOutputModel> BookPublisher { get; set; }
        ICollection<BookSeriesOutputModel> BookSeries { get; set; }
        ICollection<BookStoryOutputModel> StoryBook { get; set; }
    }
}