using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class CodeOutputModel : CodeOnlyOutputModel
    {
        public ICollection<CodeStoryOutputModel> Story { get; set; }
        public ICollection<CodeSeriesOutputModel> Series { get; set; }
    }
}
