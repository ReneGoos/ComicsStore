﻿using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;

namespace ComicsLibrary.ViewModels
{
    public class StoryViewModel : BasicTableViewModel<IStoriesService, StoryInputModel, StoryInputPatchModel, StoryOutputModel, StorySearch, StoryEditModel>
    {
        private ICollection<StoryOutputModel> _originStories;

        private void StoryViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Items" )
            {
                GetOriginStories();
            }
        }

        public IEnumerable<StoryOutputModel> OriginStories
        {
            get
            {
                if (_originStories is null)
                {
                    GetOriginStories();
                }

                return _originStories;
            }
            private set
            {
                if (value is null)
                {
                    GetOriginStories();
                    RaisePropertyChanged();
                }
            }
        }

        private void GetOriginStories()
        {
            _originStories = _items.Where(item => item.OriginStoryId is null).OrderBy(item => item.Name).ToList();
            RaisePropertyChanged("OriginStories");
        }

        public void AddStoryArtist(ICollection<StoryArtistEditModel> storyArtists, int? artistId)
        {
            Item.AddStoryArtist(storyArtists, artistId);
        }

        public void AddStoryBook(ICollection<StoryBookEditModel> storyBooks, int? bookId)
        {
            Item.AddStoryBook(storyBooks, bookId);
        }

        public void AddStoryCharacter(ICollection<StoryCharacterEditModel> storyCharacters, int? characterId)
        {
            Item.AddStoryCharacter(storyCharacters, characterId);
        }

        public void AddStoryCode(int? codeId)
        {
            Item.AddStoryCode(codeId);
        }

        public StoryViewModel(IStoriesService storiesService,
                              IMapper mapper) : base(storiesService, mapper)
        {
            PropertyChanged += StoryViewModel_PropertyChanged;
        }

        public List<StoryArtistEditModel> GetStoryArtists()
        {
            return Item.GetStoryArtists();
        }

        public List<StoryCharacterEditModel> GetStoryCharacters()
        {
            return Item.GetStoryCharacters();
        }

        public List<StoryBookEditModel> GetStoryBooks()
        {
            return Item.GetStoryBooks();
        }
    }
}
