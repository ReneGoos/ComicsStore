using ComicsStore.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class ArtistStoryOutputModel : BasicOutputModel
    {
        public string StoryName { get; set; }
        public ArtistType ArtistType { get; set; }
        public StoryType StoryType { get; set; }
        public int? StoryNumber { get; set; }
        public double? Pages { get; set; }
        public string ExtraInfo { get; set; }
    }
}
