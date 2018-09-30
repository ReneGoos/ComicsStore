﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.Data.Model
{
    public class Book
    {
        public Book()
        {
            BookSeries = new HashSet<BookSeries>();
            BookPublisher = new HashSet<BookPublisher>();
            StoryBook = new HashSet<StoryBook>();

            Active = Active.active;
            CreationDate = DateTime.Now;
            DateUpdate = DateTime.Now;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Book Name is required"), MaxLength(255)]
        public string BookName { get; set; }
        [EnumDataType(typeof(BookType), ErrorMessage = "Book type value doesn't exist within enum")]
        public BookType BookType { get; set; }
        [EnumDataType(typeof(Active), ErrorMessage = "Active value doesn't exist within enum")]
        public Active Active { get; set; }
        public int FirstYear { get; set; }
        public int? ThisYear { get; set; }
        public int? FirstPrint { get; set; }
        public string Remark { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateUpdate { get; set; }

        public ICollection<BookSeries> BookSeries { get; set; }
        public ICollection<BookPublisher> BookPublisher { get; set; }
        public ICollection<StoryBook> StoryBook { get; set; }
    }
}