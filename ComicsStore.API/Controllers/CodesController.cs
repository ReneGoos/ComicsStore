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
    public class CodesController : ControllerBase
    {
        private readonly ICodesService _codesService;

        public CodesController(ICodesService codesService)
        {
            _codesService = codesService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CodeOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromQuery] BasicSearchModel codeSearchModel)
        {
            return Ok(await _codesService.GetAsync(codeSearchModel));
        }

        [HttpGet("{id}", Name = "CodeGetAsync")]
        [ProducesResponseType(typeof(CodeOutputModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var showModel = await _codesService.GetAsync(id);

            if (showModel == null)
            {
                return NotFound();
            }

            return Ok(showModel);
        }

        [Route("{codeId}/Series")]
        [HttpGet]
        [ProducesResponseType(typeof(List<SeriesOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSeriesAsync(int codeId)
        {
            var seriesOutput = await _codesService.GetSeriesAsync(codeId);

            if (seriesOutput == null)
            {
                return NotFound();
            }

            return Ok(seriesOutput);
        }

        [Route("{codeId}/Stories")]
        [HttpGet]
        [ProducesResponseType(typeof(List<StoryOnlyOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStoriesAsync(int codeId)
        {
            var storyOutput = await _codesService.GetStoriesAsync(codeId);

            if (storyOutput == null)
            {
                return NotFound();
            }

            return Ok(storyOutput);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CodeOutputModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] CodeInputModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _codesService.AddAsync(value);

            if (result == null)
            {
                return BadRequest("Code not inserted");
            }

            return CreatedAtRoute("CodeGetAsync",
                                  new
                                  {
                                      id = result.Id
                                  },
                                  result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CodeOutputModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] CodeInputModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _codesService.UpdateAsync(id, value);

            if (result == null)
            {
                return BadRequest($"Update of code {id} failed");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _codesService.DeleteAsync(id);

            return NoContent();
        }
    }
}
