﻿using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.MiddleWare.Models.Output;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.Data.Model.Search;
using ComicsLibrary.Navigation;
using ComicsLibrary.Core;
using System;
using System.Windows.Input;
using ComicsStore.Data.Common;

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
            IsDirty |= Item.HandleSeries(oldSeriesId, series, ItemPropertyChanged);
        }

        public void DeleteSeriesFromList(int? seriesId)
        {
            IsDirty |= Item.HandleSeries(seriesId, null);
        }

        public async void HandleStory(int? storyId, int? oldStoryId)
        {
            var story = storyId.HasValue ? Mapper.Map<StoryOnlyEditModel>(await _storiesService.GetAsync(storyId.Value)) : null;
            IsDirty |= Item.HandleStory(oldStoryId, story, ItemPropertyChanged);
        }

        private void DeleteStoryFromList(int? storyId)
        {
            IsDirty |= Item.HandleStory(storyId, null);
        }

        public override void ItemChange(TableType table, int? id, ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.deleteItem:
                    switch (table)
                    {
                        case TableType.series:
                            DeleteSeriesFromList(id);
                            break;

                        case TableType.story:
                            DeleteStoryFromList(id);
                            break;
                    }
                    break;

                case ActionType.updateItem:
                    switch (table)
                    {
                        case TableType.series:
                            HandleSeries(id, id);
                            break;

                        case TableType.story:
                            HandleStory(id, id);
                            break;
                    }
                    break;
            }
        }
    }
}
