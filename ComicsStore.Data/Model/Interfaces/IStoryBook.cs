namespace ComicsStore.Data.Model.Interfaces;

public interface IStoryBook
{
    ICollection<StoryBook> StoryBook { get; set; }
}