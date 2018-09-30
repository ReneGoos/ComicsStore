using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class ArtistOutputModel : BasicOutputModel
    {
        public string ArtistName { get; set; }

        public ICollection<ArtistStoryOutputModel> StoryArtist { get; set; }
    }
}
