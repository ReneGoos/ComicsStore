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
    public class ExportMementoController : ControllerBase
    {
        private readonly IExportMementoService _exportMementoService;

        public ExportMementoController(IExportMementoService exportMementoService)
        {
            _exportMementoService = exportMementoService;
        }

        //[HttpGet]
        //[ProducesResponseType(typeof(List<ExportMementoOutputModel>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> GetAsync([FromQuery] ExportMementoSearchModel storySearchModel)
        //{
        //    var result = await _exportMementoService.GetAsync(storySearchModel);
        //    return Ok(result);
        //}

        [Route("report")]
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var export = await _exportMementoService.GetExportAsync();

            if (export == null)
            {
                return NotFound();
            }

            return Ok(export);
        }
    }
}
