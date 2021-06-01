using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input
{
    public interface ICharacterInputModel : IBasicInputModel
    {
        ICollection<StoryCharacterInputModel> StoryCharacter { get; set; }
    }
}