﻿using System.Collections.Generic;
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
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<BookOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromQuery] BasicSearch bookSearch)
        {
            return Ok(await _booksService.GetAsync(bookSearch));
        }

        [HttpGet("{id}", Name = "BookGetAsync")]
        [ProducesResponseType(typeof(BookOutputModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var showModel = await _booksService.GetAsync(id, true);

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

        [Route("{bookId}/Stories")]
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<BookStoryOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStoriesAsync(int bookId)
        {
            var storiesOutput = await _booksService.GetStoriesAsync(bookId);

            if (storiesOutput == null)
            {
                return NotFound();
            }

            return Ok(storiesOutput);
        }

        [Route("{bookId}/Series")]
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<BookSeriesOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSeriesAsync(int bookId)
        {
            var seriesOutput = await _booksService.GetSeriesAsync(bookId);

            if (seriesOutput == null)
            {
                return NotFound();
            }

            return Ok(seriesOutput);
        }

        [Route("{bookId}/Publishers")]
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<BookPublisherOutputModel>), (int)HttpStatusCode.OK)]
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
