using ComicsStore.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryOutputModel : BasicOutputModel
    {
        public StoryType StoryType { get; set; }
        public int? StoryNumber { get; set; }
        public double? Pages { get; set; }
        public string ExtraInfo { get; set; }

        public CodeOutputModel Code { get; set; }
        public ICollection<BookOutputModel> StoryBook { get; set; }
        public ICollection<CharacterOutputModel> StoryCharacter { get; set; }
        public ICollection<StoryArtistOutputModel> StoryArtist { get; set; }
    }
}
