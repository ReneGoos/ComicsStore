using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Models.Output;
using Microsoft.AspNetCore.Mvc;
using ComicsStore.MiddleWare.Services.Interfaces;

namespace ComicsStore.API.Controllers
{
    [ApiController]
    public class StoryArtistsController : ControllerBase
    {
        private readonly IStoriesService _storiesService;
        private readonly IArtistsService _artistsService;
        private readonly IStoryArtistsService _storyArtistsService;

        public StoryArtistsController(IStoriesService storiesService,  
            IArtistsService artistsService,
            IStoryArtistsService storyArtistsService)
        {
            _storiesService = storiesService;
            _artistsService = artistsService;
            _storyArtistsService = storyArtistsService;
        }

        [Route("api/Stories/{storyId}/Artists")]
        [HttpGet]
        [ProducesResponseType(typeof(List<StoryArtistOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetArtistsAsync(int storyId)
        {
            var artistOutput = await _storyArtistsService.GetSubAsync(storyId);

            if (artistOutput == null)
            {
                return NotFound();
            }

            return Ok(artistOutput);
        }

        [Route("api/Artists/{artistId}/Stories")]
        [HttpGet]
        [ProducesResponseType(typeof(List<ArtistStoryOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStoriesAsync(int artistId)
        {
            var storiesOutput = await _storyArtistsService.GetMainAsync(artistId);

            if (storiesOutput == null)
            {
                return NotFound();
            }

            return Ok(storiesOutput);
        }

        //[Route("api/Stories/{storyId}/Artists")]
        //[HttpPost]
        //[ProducesResponseType(typeof(List<StoryArtistOutputModel>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> PostArtistsAsync(int storyId, [FromBody] StoryArtistInputModel storyArtist)
        //{
        //    var artistOutput = await _storiesService.GetArtistsAsync(storyId);

        //    if (artistOutput == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(artistOutput);
        //}

    }
}
