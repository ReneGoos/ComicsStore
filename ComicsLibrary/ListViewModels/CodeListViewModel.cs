using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Services.Interfaces;

namespace ComicsLibrary.ListViewModels;

public class CodeListViewModel(ICodesService itemService) : BasicListViewModel<ICodesService,
                                                    CodeInputModel,
                                                    CodeInputModel,
                                                    CodeOutputModel,
                                                    BasicSearch>(itemService)
{
}
