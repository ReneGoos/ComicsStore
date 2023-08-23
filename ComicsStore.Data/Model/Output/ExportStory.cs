using ComicsStore.Data.Common;

namespace ComicsStore.Data.Model.Output
{
    public class ExportStory : ResultView
    {
        public string Title { get; set; }
        public int? StoryNumber { get; set; }
        public string ExtraInfo { get; set; }
        public StoryType StoryType { get; set; }
    }
}
