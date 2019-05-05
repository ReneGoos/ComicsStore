namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryCharacterOutputModel : BasicCrossOutputModel
    {
        public int CharacterId { get; set; }
                
        public CharacterOnlyOutputModel Character { get; set; }
    }
}
