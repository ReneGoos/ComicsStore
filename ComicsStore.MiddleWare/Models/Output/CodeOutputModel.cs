using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class CodeOutputModel : BasicOutputModel
    {
        public string CodeName { get; set; }

        public ICollection<SeriesOutputModel> SeriesCode { get; set; }
        public ICollection<StoryOutputModel> StoryCode { get; set; }
    }
}
