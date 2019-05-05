using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface IStoryArtistsService : IComicsStoreCrossService<ArtistStoryOutputModel, StoryArtistInputModel, StoryArtistOutputModel>
    {
    }
}