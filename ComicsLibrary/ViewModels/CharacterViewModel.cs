using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsLibrary.Navigation;
using System.Windows.Input;
using ComicsLibrary.Core;
using System;
using ComicsStore.MiddleWare.Services;

namespace ComicsLibrary.ViewModels
{
    public class CharacterViewModel : BasicTableViewModel<ICharactersService, CharacterInputModel, CharacterInputModel, CharacterOutputModel, BasicSearch, CharacterEditModel>
    {
        private readonly IStoriesService _storiesService;

        public ICommand DeleteStoryFromListCommand { get; protected set; }

        public CharacterViewModel(ICharactersService charactersService,
            IStoriesService storiesService,
            INavigationService navigationService,
            IMapper mapper) : base(charactersService, navigationService, mapper)
        {
            DeleteStoryFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteStoryFromList));
            _storiesService = storiesService;
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