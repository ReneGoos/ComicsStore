using ComicsStore.Data.Common;

namespace ComicsStore.Data.Model.Search;
public interface IViewSearch : ISearch
{
    Active? Active { get; set; }
    string Filter { get; set; }
}