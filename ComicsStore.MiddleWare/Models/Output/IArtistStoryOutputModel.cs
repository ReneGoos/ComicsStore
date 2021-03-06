﻿using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public interface IArtistStoryOutputModel
    {
        int ArtistId { get; set; }
        ICollection<string> ArtistType { get; set; }
        int StoryId { get; set; }
    }
}