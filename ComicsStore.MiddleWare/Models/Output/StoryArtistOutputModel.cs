using ComicsStore.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryArtistOutputModel : BasicOutputModel
    {
        public string ArtistName { get; set; }
        public ArtistType ArtistType { get; set; }
    }
}
