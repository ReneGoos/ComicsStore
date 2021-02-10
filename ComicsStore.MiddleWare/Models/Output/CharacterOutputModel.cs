using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class CharacterOutputModel : CharacterOnlyOutputModel
    {
        public ICollection<CharacterStoryOutputModel> StoryCharacter { get; set; }
    }
}
