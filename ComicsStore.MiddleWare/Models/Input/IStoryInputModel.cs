using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input
{
    public interface IStoryInputModel : IBasicInputModel
    {
        CodeInputModel Code { get; set; }
        int CodeId { get; set; }
        string ExtraInfo { get; set; }
        string Language { get; set; }
        StoryInputModel OriginStory { get; set; }
        int? OriginStoryId { get; set; }
        double? Pages { get; set; }
        ICollection<StoryArtistInputModel> StoryArtist { get; set; }
        ICollection<StoryBookInputModel> StoryBook { get; set; }
        ICollection<StoryCharacterInputModel> StoryCharacter { get; set; }
        decimal? StoryNumber { get; set; }
        string StoryType { get; set; }
    }
}