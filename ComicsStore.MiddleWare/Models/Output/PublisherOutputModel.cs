using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class PublisherOutputModel : BasicOutputModel
    {
        public string PublisherName { get; set; }

        public ICollection<BookOutputModel> BookPublisher { get; set; }
    }
}