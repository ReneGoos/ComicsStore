using ComicsStore.Data.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class StoryInputModel : BasicInputModel
    {
        [Required(ErrorMessage = "Story Name is required"), MaxLength(255)]
        public string StoryName { get; set; }
        [EnumDataType(typeof(StoryType), ErrorMessage = "Story type value doesn't exist within enum")]
        public StoryType StoryType { get; set; }
        public int? StoryNumber { get; set; }
        public double? Pages { get; set; }
        [MaxLength(255)]
        public string ExtraInfo { get; set; }

        public CodeInputModel Code { get; set; }
        public ICollection<BookInputModel> StoryBook { get; set; }
        public ICollection<CharacterInputModel> StoryCharacter { get; set; }
        public ICollection<StoryArtistInputModel> StoryArtist { get; set; }
    }
}
