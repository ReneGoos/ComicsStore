using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using Microsoft.AspNetCore.Mvc;
using ComicsStore.MiddleWare.Services.Interfaces;

namespace ComicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublishersService _publishersService;

        public PublishersController(IPublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<PublisherOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromQuery] BasicSearchModel publisherSearchModel)
        {
            return Ok(await _publishersService.GetAsync(publisherSearchModel));
        }

        [HttpGet("{id}", Name = "PublisherGetAsync")]
        [ProducesResponseType(typeof(PublisherOutputModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var showModel = await _publishersService.GetAsync(id);

            if (showModel == null)
            {
                return NotFound();
            }

            return Ok(showModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PublisherOutputModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] PublisherInputModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _publishersService.AddAsync(value);

            if (result == null)
            {
                return BadRequest("Publisher not inserted");
            }

            return CreatedAtRoute("PublisherGetAsync",
                                  new
                                  {
                                      id = result.Id
                                  },
                                  result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublisherOutputModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] PublisherInputModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _publishersService.UpdateAsync(id, value);

            if (result == null)
            {
                return BadRequest($"Update of publisher {id} failed");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _publishersService.DeleteAsync(id);

            return NoContent();
        }

        [Route("{publisherId}/Books")]
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<BookOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBooksAsync(int publisherId)
        {
            var bookOutput = await _publishersService.GetBooksAsync(publisherId);

            if (bookOutput == null)
            {
                return NotFound();
            }

            return Ok(bookOutput);
        }
    }
}
