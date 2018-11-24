using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.Data.Model
{
    public class Character : MainTable
    {
        public Character()
            : base()
        {
            StoryCharacter = new HashSet<StoryCharacter>();
        }

        public ICollection<StoryCharacter> StoryCharacter { get; set; }
    }
}
