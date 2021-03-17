using AutoMapper;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ComicsLibrary.EditModels
{
    public class StoryEditModel : TableEditModel
    {
        private string _storyType;
        private int? _storyNumber;
        private double? _pages;
        private string _extraInfo;
        private string _language;
        private int _codeId;
        private int? _originStoryId;
        private ICollection<StoryArtistEditModel> _storyArtist;
        private ICollection<StoryBookEditModel> _storyBook;
        private ICollection<StoryCharacterEditModel> _storyCharacter;

        public string StoryType { get => _storyType; set { _storyType = value; RaisePropertyChanged(); }}
        public int? StoryNumber { get => _storyNumber; set { _storyNumber = value; RaisePropertyChanged(); }}
        public double? Pages { get => _pages; set { _pages = value; RaisePropertyChanged(); }}
        public string ExtraInfo { get => _extraInfo; set { _extraInfo = value; RaisePropertyChanged(); }}
        public string Language { get => _language; set { _language = value; RaisePropertyChanged(); }}
        public int CodeId { get => _codeId; set { _codeId = value; RaisePropertyChanged(); }}
        public int? OriginStoryId { get => _originStoryId; set { _originStoryId = value; RaisePropertyChanged(); }}

        public ICollection<StoryArtistEditModel> StoryArtist
        {
            get => _storyArtist;
            set
            {
                _storyArtist = value;
                RaisePropertyChanged();
            }
        }

        public ICollection<StoryBookEditModel> StoryBook
        {
            get => _storyBook;
            set
            {
                _storyBook = value;
                RaisePropertyChanged();
            }
        }

        public ICollection<StoryCharacterEditModel> StoryCharacter
        {
            get => _storyCharacter;
            set
            {
                _storyCharacter = value;
                RaisePropertyChanged();
            }
        }
    }
}
