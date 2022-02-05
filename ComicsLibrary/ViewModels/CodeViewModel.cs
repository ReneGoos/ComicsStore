using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.MiddleWare.Models.Output;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.Data.Model.Search;
using System.Collections.Generic;
using ComicsLibrary.Core;

namespace ComicsLibrary.ViewModels
{
    public class CodeViewModel : BasicTableViewModel<ICodesService, CodeInputModel, CodeInputModel, CodeOutputModel, BasicSearch, CodeEditModel>
    {
        public CodeViewModel(ICodesService codesService,
            IMapper mapper) : base(codesService, mapper)
        {
        }

        public void AddSeriesCodes(ObservableChangedCollection<SeriesCodeEditModel> seriesCodes, int? seriesId)
        {
            Item.AddSeriesCodes(seriesCodes, seriesId);
        }

        public void AddStoryCodes(ObservableChangedCollection<StoryCodeEditModel> storyCodes, int? storyId)
        {
            Item.AddStoryCodes(storyCodes, storyId);
        }

        public ObservableChangedCollection<SeriesCodeEditModel> GetSeriesCodes()
        {
            return Item.GetSeriesCodes();
        }

        public ObservableChangedCollection<StoryCodeEditModel> GetStoryCodes()
        {
            return Item.GetStoryCodes();
        }
    }
}
