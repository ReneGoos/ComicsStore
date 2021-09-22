using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.MiddleWare.Models.Output;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.Data.Model.Search;
using System.Collections.Generic;

namespace ComicsLibrary.ViewModels
{
    public class CodeViewModel : BasicTableViewModel<ICodesService, CodeInputModel, CodeInputModel, CodeOutputModel, BasicSearch, CodeEditModel>
    {
        public CodeViewModel(ICodesService codesService,
            IMapper mapper) : base(codesService, mapper)
        {
        }

        public void AddSeriesCodes(int? seriesId)
        {
            Item.AddSeriesCodes(seriesId);
        }

        public void AddStoryCodes(int? storyId)
        {
            Item.AddStoryCodes(storyId);
        }

        public List<SeriesCodeEditModel> GetSeriesCodes()
        {
            return Item.GetSeriesCodes();
        }

        public List<StoryCodeEditModel> GetStoryCodes()
        {
            return Item.GetStoryCodes();
        }
    }
}
