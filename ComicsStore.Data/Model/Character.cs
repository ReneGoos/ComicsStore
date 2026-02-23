using ComicsStore.Data.Model.Interfaces;

namespace ComicsStore.Data.Model;

public class Character : MainTable, IStoryCharacter
{
    public Character()
        : base()
    {
        StoryCharacter = new HashSet<StoryCharacter>();
    }

    public ICollection<StoryCharacter> StoryCharacter { get; set; }
}
