﻿using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsLibrary.Navigation;
using System.Windows.Input;
using ComicsLibrary.Core;
using System;

namespace ComicsLibrary.ViewModels
{
    public class BookViewModel : BasicTableViewModel<IBooksService, BookInputModel, BookInputPatchModel, BookOutputModel, BasicSearch, BookEditModel>
    {
        private readonly IPublishersService _publishersService;
        private readonly ISeriesService _seriesService;
        private readonly IStoriesService _storiesService;

        public ICommand DeletePublisherFromListCommand { get; protected set; }
        public ICommand DeleteSeriesFromListCommand { get; protected set; }
        public ICommand DeleteStoryFromListCommand { get; protected set; }

        public BookViewModel(IBooksService booksService,
            IPublishersService publishersService, 
            ISeriesService seriesService,
            IStoriesService storiesService,
            INavigationService navigationService,
            IMapper mapper) : base(booksService, navigationService, mapper)
        {
            DeletePublisherFromListCommand = new RelayCommand<int?>(new Action<int?>(DeletePublisherFromList));
            DeleteSeriesFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteSeriesFromList));
            DeleteStoryFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteStoryFromList));
            _publishersService = publishersService;
            _seriesService = seriesService;
            _storiesService = storiesService;
        }

        public async void HandlePublisher(int? publisherId, int? oldPublisherId)
        {
            var publisher = publisherId.HasValue ? Mapper.Map<PublisherOnlyEditModel>(await _publishersService.GetAsync(publisherId.Value)) : null;
            Item.HandlePublisher(oldPublisherId, publisher);
        }

        private void DeletePublisherFromList(int? publisherId)
        {
            Item.HandlePublisher(publisherId, null);
        }

        public async void HandleSeries(int? seriesId, int? oldSeriesId)
        {
            var series = seriesId.HasValue ? Mapper.Map<SeriesOnlyEditModel>(await _seriesService.GetAsync(seriesId.Value)) : null;
            Item.HandleSeries(oldSeriesId, series);
        }

        private void DeleteSeriesFromList(int? seriesId)
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