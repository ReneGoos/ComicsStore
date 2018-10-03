using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class ArtistOutputModel : BasicOutputModel
    {
        public ICollection<ArtistStoryOutputModel> StoryArtist { get; set; }
    }
}
