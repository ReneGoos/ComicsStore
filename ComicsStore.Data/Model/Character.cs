using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.Data.Model
{
    public class Character
    {
        public Character()
        {
            StoryCharacter = new HashSet<StoryCharacter>();

            CreationDate = DateTime.Now;
            DateUpdate = DateTime.Now;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Character Name is required"), MaxLength(255)]
        public string Name { get; set; }
        public string Remark { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateUpdate { get; set; }

        public ICollection<StoryCharacter> StoryCharacter { get; set; }
    }
}
