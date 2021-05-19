using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class CharacterInputModel : BasicInputModel
    {
        public ICollection<StoryCharacterInputModel> StoryCharacter { get; set; }
    }
}
