﻿namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryOnlyOutputModel : BasicOutputModel
    {
        public string StoryType { get; set; }
        public int? StoryNumber { get; set; }
        public double? Pages { get; set; }
        public string ExtraInfo { get; set; }
        public string Language { get; set; }
        public int CodeId { get; set; }
        public int? OriginStoryId { get; set; }

        public CodeOnlyOutputModel Code { get; set; }
        public StoryOnlyOutputModel OriginStory { get; set; }
    }
}
