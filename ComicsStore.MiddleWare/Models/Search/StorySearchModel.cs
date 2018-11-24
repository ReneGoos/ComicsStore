using ComicsStore.Data.Model;

namespace ComicsStore.MiddleWare.Models.Search
{
    public class StorySearchModel : BasicSearchModel
    {
        public string StoryType { get; set; }
        public string ExtraInfo { get; set; }
    }
}
