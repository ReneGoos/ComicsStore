using AutoMapper;
using ComicsLibrary.Core;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using ComicsLibrary.Navigation;
using System.Collections.Generic;

namespace ComicsLibrary.ViewModels
{
    public class StoryViewModel : BasicTableViewModel<IStoriesService, StoryInputModel, StoryInputPatchModel, StoryOutputModel, StorySearchModel, StoryEditModel>
    {
        private CollectionViewSource _originStoriesViewSource;
        private string _originStoryFilter;

        private void OriginStoryFilterResults(object sender, FilterEventArgs e)
        {

            if (e.Item is StoryOutputModel story)
            {
                if (story.OriginStoryId is not null)
                {
                    e.Accepted = false;
                    return;
                }

                if (_originStoryFilter is null || _originStoryFilter.Length == 0)
                {
                    e.Accepted = true;
                    return;
                }

                e.Accepted = story.Name.Contains(_originStoryFilter, System.StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public string OriginStoryFilter
        {
            get
            {
                return _originStoryFilter;
            }
            set
            {
                Set(ref _originStoryFilter, value);
                _originStoriesViewSource.View.Refresh();
            }
        }

        public ICollectionView OriginStories
        {
            get
            {
                if (_originStoriesViewSource is null)
                    GetItemsAsync();

                return _originStoriesViewSource.View;
            }
            private set
            {
                if (value is null)
                {
                    _originStoriesViewSource = new CollectionViewSource
                    {
                        Source = _items
                    };
                    _originStoriesViewSource.Filter += OriginStoryFilterResults;
                    _originStoriesViewSource.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                    RaisePropertyChanged();
                }
            }
        }

        protected override void GetItemsAsync()
        {
            base.GetItemsAsync();
            OriginStories = null;
        }

        protected override void SaveAsync()
        {
            base.SaveAsync();
            OriginStories = null;
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
