using System;
using System.Collections.Generic;
using System.Text;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class BookPublisherInputModel : BasicCrossInputModel
    {
        public int PublisherId { get; set; }

        public PublisherInputModel Publisher { get; set; }
    }
}
