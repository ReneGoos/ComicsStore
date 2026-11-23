using ComicsLibrary.EditModels;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicsLibrary.ListViewModels;

public class ArtistListViewModel : BasicListViewModel<IArtistsService,
                                                    ArtistInputModel,
                                                    ArtistInputModel,
                                                    ArtistOutputModel,
                                                    BasicSearch>
{
    public ArtistListViewModel(IArtistsService itemService) : base(itemService)
    {
    }
}
