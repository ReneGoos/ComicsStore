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
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesService _seriesService;

        public SeriesController(ISeriesService seriesService)
        {
            _seriesService = seriesService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<SeriesOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromQuery] SeriesSearch seriesSearch)
        {
            return Ok(await _seriesService.GetAsync(seriesSearch));
        }

        [HttpGet("{id}", Name = "SeriesGetAsync")]
        [ProducesResponseType(typeof(SeriesOutputModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var showModel = await _seriesService.GetAsync(id, true);

            if (showModel == null)
            {
                return NotFound();
            }

            return Ok(showModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(SeriesOutputModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] SeriesInputModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _seriesService.AddAsync(value);

            if (result == null)
            {
                return BadRequest("Series not inserted");
            }

            return CreatedAtRoute("SeriesGetAsync",
                                  new
                                  {
                                      id = result.Id
                                  },
                                  result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SeriesOutputModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SeriesInputModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _seriesService.UpdateAsync(id, value);

            if (result == null)
            {
                return BadRequest($"Update of series {id} failed");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _seriesService.DeleteAsync(id);

            return NoContent();
        }

        [Route("{seriesId}/Books")]
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<SeriesBookOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBooksAsync(int seriesId)
        {
            var bookOutput = await _seriesService.GetBooksAsync(seriesId);

            if (bookOutput == null)
            {
                return NotFound();
            }

            return Ok(bookOutput);
        }
    }
}
