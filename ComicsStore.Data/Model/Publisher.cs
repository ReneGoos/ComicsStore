using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.Data.Model
{
    public class Publisher
    {
        public Publisher()
        {
            BookPublisher = new HashSet<BookPublisher>();
            //BronPublisher = new HashSet<BronPublisher>();

            CreationDate = DateTime.Now;
            DateUpdate = DateTime.Now;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Publisher Name is required"), MaxLength(255)]
        public string PublisherName { get; set; }
        public string Remark { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateUpdate { get; set; }

        public ICollection<BookPublisher> BookPublisher { get; set; }
        //public ICollection<BronPublisher> BronPublisher { get; set; }
    }
}