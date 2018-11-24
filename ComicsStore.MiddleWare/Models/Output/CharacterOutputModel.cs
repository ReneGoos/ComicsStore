using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class CharacterOutputModel : BasicOutputModel
    {
        public ICollection<BasicStoryOutputModel> StoryCharacter { get; set; }
    }
}
