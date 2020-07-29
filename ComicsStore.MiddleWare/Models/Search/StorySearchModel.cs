using ComicsStore.Data.Model;

namespace ComicsStore.MiddleWare.Models.Search
{
    public class StorySearchModel : BasicSearchModel
    {
        public int? CodeId { get; set; }
        public StoryType? StoryType { get; set; }
        public string ExtraInfo { get; set; }
    }
}
