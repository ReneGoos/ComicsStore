using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface ICodesService : IComicsStoreService<CodeInputModel, CodeInputModel, CodeOutputModel, BasicSearchModel>
    {
        Task<List<StoryOnlyOutputModel>> GetStoriesAsync(int codeId);
        Task<List<SeriesOutputModel>> GetSeriesAsync(int codeId);
    }
}