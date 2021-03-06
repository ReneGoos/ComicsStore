﻿using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class PublisherOutputModel : PublisherOnlyOutputModel, IPublisherOutputModel
    {
        public ICollection<PublisherBookOutputModel> BookPublisher { get; set; }
    }
}