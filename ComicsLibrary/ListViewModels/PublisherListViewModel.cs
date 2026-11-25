using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Services.Interfaces;

namespace ComicsLibrary.ListViewModels;

public class PublisherListViewModel(IPublishersService itemService) : BasicListViewModel<IPublishersService,
                                                    PublisherInputModel,
                                                    PublisherInputModel,
                                                    PublisherOutputModel,
                                                    BasicSearch>(itemService)
{
}
