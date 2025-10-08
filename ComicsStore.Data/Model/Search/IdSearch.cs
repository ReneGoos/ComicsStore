namespace ComicsStore.Data.Model.Search;

public class IdSearch : ViewSearch, IViewSearch
{
    public int? ArtistId { get; set; }
    public int? BookId { get; set; }
    public int? CharacterId { get; set; }
    public int? CodeId { get; set; }
    public int? PublisherId { get; set; }
    public int? SeriesId { get; set; }
    public int? StoryId { get; set; }
}
