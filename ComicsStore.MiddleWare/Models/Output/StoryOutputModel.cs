using ComicsStore.Data.Model;
using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryOutputModel : BasicOutputModel
    {
        public string StoryType { get; set; }
        public int? StoryNumber { get; set; }
        public double? Pages { get; set; }
        public string ExtraInfo { get; set; }

        public int CodeId { get; set; }
        public CodeOnlyOutputModel Code { get; set; }

        public ICollection<BasicBookOutputModel> StoryBook { get; set; }
        public ICollection<StoryCharacterOutputModel> StoryCharacter { get; set; }
        public ICollection<StoryArtistOutputModel> StoryArtist { get; set; }
    }
}
