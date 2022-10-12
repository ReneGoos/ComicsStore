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

namespace ComicsLibrary.ViewModels
{
    public class CharacterViewModel : BasicTableViewModel<ICharactersService, CharacterInputModel, CharacterInputModel, CharacterOutputModel, BasicSearch, CharacterEditModel>
    {
        public ICommand DeleteStoryFromListCommand { get; protected set; }

        public CharacterViewModel(ICharactersService charactersService,
            INavigationService navigationService,
            IMapper mapper) : base(charactersService, navigationService, mapper)
        {
            DeleteStoryFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteStoryFromList));
        }

        public void AddStoryCharacter(int? storyId, int? oldStoryId)
        {
            Item.AddStoryCharacter(storyId, oldStoryId);
        }

        private void DeleteStoryFromList(int? storyId)
        {
            Item.AddStoryCharacter(null, storyId);
        }
    }
}