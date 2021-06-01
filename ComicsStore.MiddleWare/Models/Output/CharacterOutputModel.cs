using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class CharacterOutputModel : CharacterOnlyOutputModel, ICharacterOutputModel
    {
        public ICollection<CharacterStoryOutputModel> StoryCharacter { get; set; }
    }
}
