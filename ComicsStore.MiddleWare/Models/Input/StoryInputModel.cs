using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class StoryInputModel : BasicInputModel
    {
        public string StoryType { get; set; }
        public int? StoryNumber { get; set; }
        public double? Pages { get; set; }
        [MaxLength(255)]
        public string ExtraInfo { get; set; }

        public int CodeId { get; set; }
        public CodeInputModel Code { get; set; }

        public ICollection<StoryBookInputModel> StoryBook { get; set; }
        public ICollection<StoryCharacterInputModel> StoryCharacter { get; set; }
        public ICollection<StoryArtistInputModel> StoryArtist { get; set; }
    }
}
