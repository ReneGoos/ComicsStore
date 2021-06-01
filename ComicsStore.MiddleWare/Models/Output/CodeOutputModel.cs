using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class CodeOutputModel : CodeOnlyOutputModel, ICodeOutputModel
    {
        public ICollection<CodeStoryOutputModel> Story { get; set; }
        public ICollection<CodeSeriesOutputModel> Series { get; set; }
    }
}
