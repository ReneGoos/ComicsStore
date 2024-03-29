﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class StoryInputModel : BasicInputModel, IStoryInputModel
    {
        public string StoryType { get; set; }
        public decimal? StoryNumber { get; set; }
        public double? Pages { get; set; }
        [MaxLength(255)]
        public string ExtraInfo { get; set; }

        public int CodeId { get; set; }
        public CodeInputModel Code { get; set; }
        public int? OriginStoryId { get; set; }
        public string Language { get; set; }

        public StoryInputModel OriginStory { get; set; }

        public ICollection<StoryBookInputModel> StoryBook { get; set; }
        public ICollection<StoryCharacterInputModel> StoryCharacter { get; set; }
        public ICollection<StoryArtistInputModel> StoryArtist { get; set; }
    }
}
