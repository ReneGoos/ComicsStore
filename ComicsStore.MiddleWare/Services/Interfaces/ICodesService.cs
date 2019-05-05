using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface ICodesService : IComicsStoreService<CodeInputModel, CodeInputModel, CodeOutputModel, BasicSearchModel>
    {
    }
}