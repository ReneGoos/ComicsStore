using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class CodeOutputModel : BasicOutputModel
    {
        public ICollection<StoryOutputModel> Story { get; set; }
        public ICollection<SeriesOutputModel> Series { get; set; }
    }
}
