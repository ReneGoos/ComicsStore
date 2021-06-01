using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface ICodesService : IComicsStoreService<CodeInputModel, CodeInputModel, CodeOutputModel, BasicSearch>
    {
        Task<ICollection<CodeStoryOutputModel>> GetStoriesAsync(int codeId);
        Task<ICollection<CodeSeriesOutputModel>> GetSeriesAsync(int codeId);
    }
}