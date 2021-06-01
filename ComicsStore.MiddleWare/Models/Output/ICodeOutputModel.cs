using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public interface ICodeOutputModel
    {
        ICollection<CodeSeriesOutputModel> Series { get; set; }
        ICollection<CodeStoryOutputModel> Story { get; set; }
    }
}