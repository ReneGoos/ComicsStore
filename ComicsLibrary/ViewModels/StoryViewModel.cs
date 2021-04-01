using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.ComponentModel;
using System.Windows.Data;

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
        }


        protected override void GetItemsAsync()
        {
            base.GetItemsAsync();

            _originStoriesViewSource = new CollectionViewSource
            {
                Source = _items
            };
            _originStoriesViewSource.Filter += OriginStoryFilterResults;
            _originStoriesViewSource.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        public StoryViewModel(IStoriesService storiesService,
            IMapper mapper) : base(storiesService, mapper)
        {
        }
    }
}
