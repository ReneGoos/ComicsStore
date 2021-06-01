using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
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
        [ProducesResponseType(typeof(ICollection<ExportBooksOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromQuery] StorySeriesSearch storySearch)
        {
            var result = await _exportBooksService.GetAsync(storySearch);
            return Ok(result);
        }

        [Route("report")]
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReportAsync([FromQuery] StorySeriesSearch storySearch)
        {
            var export = await _exportBooksService.GetExportAsync(storySearch);

            if (export == null)
            {
                return NotFound();
            }

            return Ok(export);
        }
    }
}
