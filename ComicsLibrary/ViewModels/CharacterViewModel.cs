using AutoMapper;
using ComicsLibrary.Core;
using ComicsLibrary.EditModels;
using ComicsLibrary.ListViewModels;
using ComicsLibrary.Navigation;
using ComicsStore.Data.Common;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Services.Interfaces;
using SoftGoosR.Windows.Core;
using System.Windows.Input;

namespace ComicsLibrary.ViewModels;

public class CharacterViewModel : BasicTableViewModel<ICharactersService, CharacterInputModel, CharacterInputModel, CharacterOutputModel, BasicSearch, CharacterEditModel>
{
    private readonly IStoriesService _storiesService;

    public ICommand DeleteStoryFromListCommand { get; protected set; }

    public CharacterViewModel(ICharactersService charactersService,
        IStoriesService storiesService,
        INavigationService navigationService,
        IMapper mapper,
        IBasicListViewModel<CharacterOutputModel> characters) : base(charactersService, navigationService, mapper, characters)
    {
        DeleteStoryFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteStoryFromList));
        _storiesService = storiesService;
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