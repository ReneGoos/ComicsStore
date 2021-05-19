using System.Collections.Generic;

namespace ComicsStore.Data.Model.Interfaces
{
    public interface IStoryCharacter
    {
        ICollection<StoryCharacter> StoryCharacter { get; set; }
    }
}