﻿namespace ComicsStore.MiddleWare.Models.Output
{
    public class PublisherBookOutputModel : BookOnlyOutputModel
    {
        public int BookId { get; set; }
        public int PublisherId { get; set; }
    }
}