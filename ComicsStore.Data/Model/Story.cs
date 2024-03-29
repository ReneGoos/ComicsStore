﻿using ComicsStore.Data.Common;
using ComicsStore.Data.Model.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicsStore.Data.Model
{
    public class Story : MainTable, IStoryArtist, IStoryCharacter, IStoryBook
    {
        public Story()
            : base()
        {
            StoryFromOrigin = new HashSet<Story>();
            StoryBook = new HashSet<StoryBook>();
            StoryCharacter = new HashSet<StoryCharacter>();
            StoryArtist = new HashSet<StoryArtist>();
        }

        [EnumDataType(typeof(StoryType), ErrorMessage = "Story type value doesn't exist within enum")]
        public StoryType StoryType { get; set; }
        public decimal? StoryNumber { get; set; }
        public double? Pages { get; set; }
        [MaxLength(255)]
        public string ExtraInfo { get; set; }
        public int CodeId { get; set; }

        public Code Code { get; set; }
        public int? OriginStoryId { get; set; }

        [ForeignKey("OriginStoryId")]
        public Story OriginStory { get; set; }
        public string Language { get; set; }

        public ICollection<Story> StoryFromOrigin { get; set; }
        public ICollection<StoryBook> StoryBook { get; set; }
        public ICollection<StoryCharacter> StoryCharacter { get; set; }
        public ICollection<StoryArtist> StoryArtist { get; set; }
    }
}
