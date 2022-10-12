using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.MiddleWare.Models.Output;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.Data.Model.Search;
using ComicsLibrary.Navigation;
using ComicsLibrary.Core;
using System;
using System.Windows.Input;

namespace ComicsLibrary.ViewModels
{
    public class CodeViewModel : BasicTableViewModel<ICodesService, CodeInputModel, CodeInputModel, CodeOutputModel, BasicSearch, CodeEditModel>
    {
        public ICommand DeleteSeriesFromListCommand { get; protected set; }
        public ICommand DeleteStoryFromListCommand { get; protected set; }

        public CodeViewModel(ICodesService codesService,
            INavigationService navigationService,
            IMapper mapper) : base(codesService, navigationService, mapper)
        {
            DeleteSeriesFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteSeriesFromList));
            DeleteStoryFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteStoryFromList));
        }

        public void AddSeriesCodes(int? seriesId, int? oldSeriesId)
        {
            Item.AddSeriesCodes(seriesId, oldSeriesId);
        }

        public void DeleteSeriesFromList(int? seriesId)
        {
            Item.AddSeriesCodes(null, seriesId);
        }

        public void AddStoryCodes(int? storyId, int? oldStoryId)
        {
            Item.AddStoryCodes(storyId, oldStoryId);
        }

        public void DeleteStoryFromList(int? storyId)
        {
            Item.AddStoryCodes(null, storyId);
        }
    }
}
