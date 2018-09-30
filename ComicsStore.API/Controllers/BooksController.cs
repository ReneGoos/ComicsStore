using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IComicsStoreService<BookInputModel, BookOutputModel, BasicSearchModel> _booksService;

        public BooksController(IComicsStoreService<BookInputModel, BookOutputModel, BasicSearchModel> booksService)
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

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _booksService.DeleteAsync(id);

            return NoContent();
        }
    }
}
