using ComicsStore.Data.Common;

namespace ComicsStore.Data.Model.Search
{
    public class ViewSearch : IViewSearch
    {
        public Active? Active { get; set; }
        public string Filter { get; set; }
    }
}
