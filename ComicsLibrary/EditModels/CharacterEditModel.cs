using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;
using ComicsStore.MiddleWare.Models.Output;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ComicsLibrary.EditModels
{
    public class CharacterEditModel : TableEditModel
    {
        private ICollection<StoryCharacterEditModel> _storyCharacters;

        public ICollection<StoryCharacterEditModel> StoryCharacters
        {
            get => _storyCharacters;
            set
            {
                _storyCharacters = value;
                RaisePropertyChanged();
            }
        }
    }
}
