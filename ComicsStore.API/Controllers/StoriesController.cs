using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using Microsoft.AspNetCore.Mvc;
using ComicsStore.MiddleWare.Services.Interfaces;

namespace ComicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IStoriesService _storiesService;

        public StoriesController(IStoriesService storiesService)
        {
            _storiesService = storiesService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<StoryOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromQuery] StorySearch storySearch)
        {
            var result = await _storiesService.GetAsync(storySearch);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "StoryGetAsync")]
        [ProducesResponseType(typeof(StoryOutputModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var showModel = await _storiesService.GetAsync(id, true);

            if (showModel == null)
            {
                return NotFound();
            }

            return Ok(showModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(StoryOutputModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] StoryInputModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _storiesService.AddAsync(value);

            if (result == null)
            {
                return BadRequest("Story not inserted");
            }

            return CreatedAtRoute("StoryGetAsync",
                                  new
                                  {
                                      id = result.Id
                                  },
                                  result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(StoryOutputModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] StoryInputModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _storiesService.UpdateAsync(id, value);

            if (result == null)
            {
                return BadRequest($"Update of story {id} failed");
            }

            return Ok(result);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(StoryOutputModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PatchAsync(int id, [FromBody] StoryInputPatchModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _storiesService.PatchAsync(id, value);

            if (result == null)
            {
                return BadRequest($"Update of story {id} failed");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _storiesService.DeleteAsync(id);

            return NoContent();
        }

        [Route("api/Stories/{storyId}/Artists")]
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<StoryArtistOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetArtistsAsync(int storyId)
        {
            var artistOutput = await _storiesService.GetArtistsAsync(storyId);

            if (artistOutput == null)
            {
                return NotFound();
            }

            return Ok(artistOutput);
        }

        [Route("{storyId}/Characters")]
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<StoryCharacterOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCharacterAsync(int storyId)
        {
            var artistOutput = await _storiesService.GetCharactersAsync(storyId);

            if (artistOutput == null)
            {
                return NotFound();
            }

            return Ok(artistOutput);
        }

        [Route("{storyId}/Books")]
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<BookOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBookAsync(int storyId)
        {
            var bookOutput = await _storiesService.GetBooksAsync(storyId);

            if (bookOutput == null)
            {
                return NotFound();
            }

            return Ok(bookOutput);
        }
    }
}
