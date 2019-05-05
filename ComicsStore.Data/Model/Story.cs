using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.Data.Model
{
    public class Story : MainTable
    {
        public Story()
            : base()
        {
            StoryBook = new HashSet<StoryBook>();
            StoryCharacter = new HashSet<StoryCharacter>();
            StoryArtist = new HashSet<StoryArtist>();
        }

        [EnumDataType(typeof(StoryType), ErrorMessage = "Story type value doesn't exist within enum")]
        public StoryType StoryType { get; set; }
        public int? StoryNumber { get; set; }
        public double? Pages { get; set; }
        [MaxLength(255)]
        public string ExtraInfo { get; set; }
        public int CodeId { get; set; }

        public Code Code { get; set; }
        public ICollection<StoryBook> StoryBook { get; set; }
        public ICollection<StoryCharacter> StoryCharacter { get; set; }
        public ICollection<StoryArtist> StoryArtist { get; set; }
    }
}
