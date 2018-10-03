using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.Data.Model
{
    public class Story
    {
        public Story()
        {
            StoryBook = new HashSet<StoryBook>();
            StoryCharacter = new HashSet<StoryCharacter>();
            StoryArtist = new HashSet<StoryArtist>();

            CreationDate = DateTime.Now;
            DateUpdate = DateTime.Now;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Story Name is required"), MaxLength(255)]
        public string Name { get; set; }
        [EnumDataType(typeof(StoryType), ErrorMessage = "Story type value doesn't exist within enum")]
        public StoryType StoryType { get; set; }
        public int? StoryNumber { get; set; }
        public double? Pages { get; set; }
        [MaxLength(255)]
        public string ExtraInfo { get; set; }
        public string Remark { get; set; }
        public int CodeId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateUpdate { get; set; }

        public Code Code { get; set; }
        public ICollection<StoryBook> StoryBook { get; set; }
        public ICollection<StoryCharacter> StoryCharacter { get; set; }
        public ICollection<StoryArtist> StoryArtist { get; set; }
    }
}
