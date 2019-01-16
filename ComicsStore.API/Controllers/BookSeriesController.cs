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
    [ApiController]
    public class BookSeriesController : ControllerBase
    {
        private readonly IBooksService _booksService;
        private readonly ISeriesService _seriesService;
        private readonly IBookSeriesService _bookSeriesService;

        public BookSeriesController(IBooksService booksService,
            ISeriesService seriesService,
            IBookSeriesService bookSeriesService)
        {
            _booksService = booksService;
            _seriesService = seriesService;
            _bookSeriesService = bookSeriesService;
        }

        [Route("api/Books/{bookId}/Series")]
        [HttpGet]
        [ProducesResponseType(typeof(List<BookSeriesOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSeriesAsync(int bookId)
        {
            var seriesOutput = await _bookSeriesService.GetSubAsync(bookId);

            if (seriesOutput == null)
            {
                return NotFound();
            }

            return Ok(seriesOutput);
        }

        [Route("api/Series/{seriesId}/Books")]
        [HttpGet]
        [ProducesResponseType(typeof(List<SeriesBookOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBooksAsync(int seriesId)
        {
            var booksOutput = await _bookSeriesService.GetMainAsync(seriesId);

            if (booksOutput == null)
            {
                return NotFound();
            }

            return Ok(booksOutput);
        }

        [Route("api/Books/{bookId}/Series")]
        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostSubAsync(int bookId, [FromBody] List<BookSeriesInputModel> bookSeries)
        {
            var result = await _bookSeriesService.AddSubAsync(bookId, bookSeries);

            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
