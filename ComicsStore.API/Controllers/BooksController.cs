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
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BookOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromQuery] BasicSearchModel bookSearchModel)
        {
            return Ok(await _booksService.GetAsync(bookSearchModel));
        }

        [HttpGet("{id}", Name = "BookGetAsync")]
        [ProducesResponseType(typeof(BookOutputModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var showModel = await _booksService.GetAsync(id);

            if (showModel == null)
            {
                return NotFound();
            }

            return Ok(showModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BookOutputModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] BookInputModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _booksService.AddAsync(value);

            if (result == null)
            {
                return BadRequest("Book not inserted");
            }

            return CreatedAtRoute("BookGetAsync",
                                  new
                                  {
                                      id = result.Id
                                  },
                                  result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BookOutputModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] BookInputModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _booksService.UpdateAsync(id, value);

            if (result == null)
            {
                return BadRequest($"Update of book {id} failed");
            }

            return Ok(result);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(BookOutputModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PatchAsync(int id, [FromBody] BookInputPatchModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _booksService.PatchAsync(id, value);

            if (result == null)
            {
                return BadRequest($"Update of book {id} failed");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _booksService.DeleteAsync(id);

            return NoContent();
        }

        [Route("{bookId}/Publishers")]
        [HttpGet]
        [ProducesResponseType(typeof(List<BookPublisherOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPublisherAsync(int bookId)
        {
            var publisherOutput = await _booksService.GetPublishersAsync(bookId);

            if (publisherOutput == null)
            {
                return NotFound();
            }

            return Ok(publisherOutput);
        }
    }
}
