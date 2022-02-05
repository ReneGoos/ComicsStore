using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.Collections.Generic;
using ComicsLibrary.Core;

namespace ComicsLibrary.ViewModels
{
    public class CharacterViewModel : BasicTableViewModel<ICharactersService, CharacterInputModel, CharacterInputModel, CharacterOutputModel, BasicSearch, CharacterEditModel>
    {
        public CharacterViewModel(ICharactersService charactersService,
            IMapper mapper) : base(charactersService, mapper)
        {
        }

        public void AddStoryCharacter(ObservableChangedCollection<StoryCharacterEditModel> storyCharacters, int? storyId)
        {
            Item.AddStoryCharacter(storyCharacters, storyId);
        }

        public ObservableChangedCollection<StoryCharacterEditModel> GetStoryCharacters()
        {
            return Item.GetStoryCharacters();
        }
    }
}