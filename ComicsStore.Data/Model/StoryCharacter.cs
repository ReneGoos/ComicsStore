namespace ComicsStore.Data.Model
{
    public class StoryCharacter : CrossTable
    {
        public StoryCharacter() : base()
        {
        }

        public int StoryId { get; set; }
        public int CharacterId { get; set; }

        public Character Character { get; set; }
        public Story Story { get; set; }
    }
}