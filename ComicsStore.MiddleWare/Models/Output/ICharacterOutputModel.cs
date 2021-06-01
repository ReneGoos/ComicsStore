using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public interface ICharacterOutputModel : IBasicOutputModel
    {
        ICollection<CharacterStoryOutputModel> StoryCharacter { get; set; }
    }
}