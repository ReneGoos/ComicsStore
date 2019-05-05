using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using Microsoft.AspNetCore.Mvc;
using ComicsStore.MiddleWare.Services.Interfaces;

namespace ComicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportBooksController : ControllerBase
    {
        private readonly IExportBooksService _exportBooksService;

        public ExportBooksController(IExportBooksService exportBooksService)
        {
            _exportBooksService = exportBooksService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ExportBooksOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromQuery] StorySeriesSearchModel storySearchModel)
        {
            var result = await _exportBooksService.GetAsync(storySearchModel);
            return Ok(result);
        }

        [Route("report")]
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReportAsync([FromQuery] StorySeriesSearchModel storySearchModel)
        {
            var export = await _exportBooksService.GetExportAsync(storySearchModel);

            if (export == null)
            {
                return NotFound();
            }

            return Ok(export);
        }
    }
}
