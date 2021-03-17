using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface IArtistsService : IComicsStoreService<ArtistInputModel, ArtistInputModel, ArtistOutputModel, BasicSearchModel>
    {
        Task<ICollection<ArtistStoryOutputModel>> GetStoriesAsync(int artistId);
    }
}