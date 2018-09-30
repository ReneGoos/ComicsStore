using System;

namespace ComicsStore.Data.Model
{
    public class StoryCharacter
    {
        public StoryCharacter()
        {
            CreationDate = DateTime.Now;
            DateUpdate = DateTime.Now;
        }

        public int StoryId { get; set; }
        public int CharacterId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateUpdate { get; set; }

        public Character Character { get; set; }
        public Story Story { get; set; }
    }
}