using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComicsStore.Data.Model
{
    public class ExportStory
    {
        public string Title { get; set; }
        public int? StoryNumber { get; set; }
        public string ExtraInfo { get; set; }
        public StoryType StoryType { get; set; }
    }
}
