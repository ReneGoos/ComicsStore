﻿using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryOutputModel : StoryOnlyOutputModel, IStoryOutputModel
    {
        public CodeOnlyOutputModel Code { get; set; }
        public StoryOnlyOutputModel OriginStory { get; set; }
        public ICollection<StoryBookOutputModel> StoryBook { get; set; }
        public ICollection<StoryCharacterOutputModel> StoryCharacter { get; set; }
        public ICollection<StoryArtistOutputModel> StoryArtist { get; set; }
        public ICollection<StoryOriginOutputModel> StoryFromOrigin { get; set; }
    }
}
