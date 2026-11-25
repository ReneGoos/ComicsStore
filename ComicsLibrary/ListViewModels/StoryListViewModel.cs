using ComicsLibrary.EditModels;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.Windows.Documents;

namespace ComicsLibrary.ListViewModels;

public class StoryListViewModel(IStoriesService itemService) : BasicListViewModel<IStoriesService,
                                                    StoryInputModel,
                                                    StoryInputPatchModel,
                                                    StoryOutputModel,
                                                    StorySearch>(itemService)
{
    private ICollection<StoryOutputModel> _originStories;

    public IEnumerable<StoryOutputModel> OriginStories
    {
        get
        {
            if (_originStories is null)
            {
                LoadOriginStories();
            }

            return _originStories;
        }
        private set
        {
            if (value is null)
            {
                LoadOriginStories();
            }
            else
            {
                _originStories = [.. value];
            }

            StoryOnlyEditModel.ListUpdating = true;
            RaisePropertyChanged();
            StoryOnlyEditModel.ListUpdating = false;
        }
    }
    public void LoadOriginStories()
    {
        OriginStories = Items.Where(item => item.OriginStoryId is null).OrderBy(item => item.Name);
        //do not use here: RaisePropertyChanged("OriginStories");
    }
}
