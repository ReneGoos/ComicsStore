using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class CharacterInputModel : BasicInputModel, ICharacterInputModel
    {
        public ICollection<StoryCharacterInputModel> StoryCharacter { get; set; }
    }
}
