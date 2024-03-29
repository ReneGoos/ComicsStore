﻿using ComicsStore.Data.Common;
using ComicsStore.Data.Model.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.Data.Model
{
    public class Book : MainTable, IBookPublisher, IBookSeries, IStoryBook
    {
        public Book()
            : base()
        {
            BookSeries = new HashSet<BookSeries>();
            BookPublisher = new HashSet<BookPublisher>();
            StoryBook = new HashSet<StoryBook>();

            Active = Active.active;
            FirstPrint = YesNoInd.no;
            Signed = YesNoInd.no;
            Checked = YesNoInd.no;
            CoverType = CoverType.softcover;
        }

        [EnumDataType(typeof(BookType), ErrorMessage = "Book type value doesn't exist within enum")]
        public BookType BookType { get; set; }
        [EnumDataType(typeof(Active), ErrorMessage = "Active value doesn't exist within enum")]
        public Active Active { get; set; }
        public int FirstYear { get; set; }
        public int? ThisYear { get; set; }
        public YesNoInd FirstPrint { get; set; }
        public YesNoInd Signed { get; set; }
        public YesNoInd Checked { get; set; }
        public CoverType CoverType { get; set; }

        public ICollection<BookSeries> BookSeries { get; set; }
        public ICollection<BookPublisher> BookPublisher { get; set; }
        public ICollection<StoryBook> StoryBook { get; set; }
    }
}