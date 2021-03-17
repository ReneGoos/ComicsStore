using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsLibrary.Helpers;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ComicsLibrary.ViewModels
{
    public class StoryViewModel : BasicViewModel
    {
        private readonly IStoriesService _storyService;
        private StoryEditModel _story;

        private ICollection<StoryOutputModel> _stories;
        private CollectionViewSource _storiesViewSource;
        private CollectionViewSource _originStoriesViewSource;
        private string _storyFilter;
        private string _originStoryFilter;

        private void StoryFilterResults(object sender, FilterEventArgs e)
        {
            if (_storyFilter is null || _storyFilter.Length == 0)
            {
                e.Accepted = true;
                return;
            }

            if (e.Item is StoryOutputModel story)
            {
                e.Accepted = story.Name.Contains(_storyFilter, System.StringComparison.InvariantCultureIgnoreCase);
            }
        }

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

        public string StoryFilter
        {
            get
            {
                return _storyFilter;
            }
            set
            {
                _storyFilter = value;
                _storiesViewSource.View.Refresh();
                RaisePropertyChanged();
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
                _originStoryFilter = value;
                _originStoriesViewSource.View.Refresh();
                RaisePropertyChanged();
            }
        }
        public ICollectionView Stories
        {
            get
            {
                if (_storiesViewSource is null)
                    GetStoriesAsync();

                return _storiesViewSource.View;
            }
        }

        public ICollectionView OriginStories
        {
            get
            {
                if (_originStoriesViewSource is null)
                    GetStoriesAsync();

                return _originStoriesViewSource.View;
            }
        }


        private async void GetStoriesAsync()
        {
            _stories = await _storyService.GetAsync(new ComicsStore.MiddleWare.Models.Search.StorySearchModel { });
            _storiesViewSource = new CollectionViewSource
            {
                Source = _stories
            };
            _storiesViewSource.Filter += StoryFilterResults;
            _storiesViewSource.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        
            _originStoriesViewSource = new CollectionViewSource
            {
                Source = _stories
            };
            _originStoriesViewSource.Filter += OriginStoryFilterResults;
            _originStoriesViewSource.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        public StoryViewModel(IStoriesService storiesService,
            IMapper mapper) : base(mapper)
        {
            _storyService = storiesService;

            GetCommand = new RelayCommand<int>(new Action<int>(GetStoryAsync));
            NewCommand = new RelayCommand(new Action(NewStory));
            SaveCommand = new RelayCommand(new Action(SaveAsync));
            CancelSaveCommand = new RelayCommand(new Action(CancelSaveAsync));

            NewStory();
        }

        private void CancelSaveAsync()
        {
            if (Story.Id is null)
            {
                NewStory();
            }
            else
            {
                GetStoryAsync(Story.Id.Value);
            }
        }

        private async void SaveAsync()
        {
            var storyInput = Mapper.Map<StoryInputModel>(Story);
            var storyOut =  (Story.Id is null) ? await _storyService.AddAsync(storyInput) : await _storyService.UpdateAsync(Story.Id.Value, storyInput);
            Story = Mapper.Map<StoryEditModel>(storyOut);
            Story.PropertyChanged += Story_PropertyChanged;
            IsDirty = false;
        }

        private void NewStory()
        {
            Story = new StoryEditModel();
            Story.PropertyChanged += Story_PropertyChanged;
            IsDirty = false;
        }

        private async void GetStoryAsync(int id)
        {
            Story = Mapper.Map<StoryEditModel>(await _storyService.GetAsync(id));
            Story.PropertyChanged += Story_PropertyChanged;
            IsDirty = false;
            //return Story is not null;
        }

        private void Story_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Id":
                    if (Story.Id.HasValue)
                    {
                        GetStoryAsync(Story.Id.Value);
                    }
                    else
                    {
                        NewStory();
                    }

                    break;
                case "Name":
                    GetStoriesAsync();
                    goto default;
                default:
                    IsDirty = true;
                    break;
            }
        }

        public StoryEditModel Story
        {
            get => _story;
            private set
            {
                _story = value;
                RaisePropertyChanged();
            }
        }
    }
}
