﻿using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class StoryArtistInputModel
    {
        public int ArtistId { get; set; }
        public List<string> ArtistType { get; set; }

        public ArtistInputModel Artist { get; set; }
    }
}
