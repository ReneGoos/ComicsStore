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
        private readonly ISeriesService _seriesService;
        private readonly IStoriesService _storiesService;

        public ICommand DeleteSeriesFromListCommand { get; protected set; }
        public ICommand DeleteStoryFromListCommand { get; protected set; }

        public CodeViewModel(ICodesService codesService,
            ISeriesService seriesService,
            IStoriesService storiesService,
            INavigationService navigationService,
            IMapper mapper) : base(codesService, navigationService, mapper)
        {
            DeleteSeriesFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteSeriesFromList));
            DeleteStoryFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteStoryFromList));
            _seriesService = seriesService;
            _storiesService = storiesService;
        }

        public async void HandleSeries(int? seriesId, int? oldSeriesId)
        {
            var series = seriesId.HasValue ? Mapper.Map<SeriesOnlyEditModel>(await _seriesService.GetAsync(seriesId.Value)) : null;
            Item.HandleSeries(oldSeriesId, series);
        }

        public void DeleteSeriesFromList(int? seriesId)
        {
            Item.HandleSeries(seriesId, null);
        }

        public async void HandleStory(int? storyId, int? oldStoryId)
        {
            var story = storyId.HasValue ? Mapper.Map<StoryOnlyEditModel>(await _storiesService.GetAsync(storyId.Value)) : null;
            Item.HandleStory(oldStoryId, story);
        }

        private void DeleteStoryFromList(int? storyId)
        {
            Item.HandleStory(storyId, null);
        }
    }
}
