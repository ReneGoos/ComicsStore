using ComicsStore.Data.Model;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryCharacterOutputModel
    {
        public int CharacterId { get; set; }
                
        public CharacterOnlyOutputModel Character { get; set; }
    }
}
