using ComicsStore.Data.Common;

namespace ComicsStore.Data.Model.Search
{
    public class StorySearch : BasicSearch
    {
        public int? CodeId { get; set; }
        public StoryType? StoryType { get; set; }
        public string ExtraInfo { get; set; }
    }
}
