using ComicsStore.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class BookInputModel : BasicInputModel
    {
        [Required(ErrorMessage = "Book Name is required"), MaxLength(255)]
        public string BookName { get; set; }
        [EnumDataType(typeof(BookType), ErrorMessage = "Book type value doesn't exist within enum")]
        public BookType BookType { get; set; }
        [EnumDataType(typeof(Active), ErrorMessage = "Active value doesn't exist within enum")]
        public Active Active { get; set; }
        public int FirstYear { get; set; }
        public int? ThisYear { get; set; }
        public int? FirstPrint { get; set; }

        public ICollection<BookSeriesInputModel> BookSeries { get; set; }
        public ICollection<PublisherInputModel> BookPublisher { get; set; }
        public ICollection<StoryInputModel> StoryBook { get; set; }
    }
}