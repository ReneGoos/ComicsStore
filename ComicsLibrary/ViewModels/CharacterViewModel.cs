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
using ComicsStore.Data.Common;

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
            IsDirty = IsDirty || Item.HandleStory(oldStoryId, story);
        }

        private void DeleteStoryFromList(int? storyId)
        {
            IsDirty = IsDirty || Item.HandleStory(storyId, null);
        }

        public override void ItemChange(TableType table, int? id, ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.deleteItem:
                    switch (table)
                    {
                        case TableType.story:
                            DeleteStoryFromList(id);
                            break;
                    }
                    break;

                case ActionType.updateItem:
                    switch (table)
                    {
                        case TableType.story:
                            HandleStory(id, id);
                            break;
                    }
                    break;
            }
        }
    }
}