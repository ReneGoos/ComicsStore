﻿using System;
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
    public class ArtistsController : ControllerBase
    {
        private readonly IComicsStoreService<ArtistInputModel, ArtistOutputModel, BasicSearchModel> _artistsService;

        public ArtistsController(IComicsStoreService<ArtistInputModel, ArtistOutputModel, BasicSearchModel> artistsService)
        {
            _artistsService = artistsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ArtistOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromQuery] BasicSearchModel artistSearchModel)
        {
            return Ok(await _artistsService.GetAsync(artistSearchModel));
        }

        [HttpGet("{id}", Name = "ArtistGetAsync")]
        [ProducesResponseType(typeof(ArtistOutputModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var showModel = await _artistsService.GetAsync(id);

            if (showModel == null)
            {
                return NotFound();
            }

            return Ok(showModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ArtistOutputModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] ArtistInputModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _artistsService.AddAsync(value);

            if (result == null)
            {
                return BadRequest("Artist not inserted");
            }

            return CreatedAtRoute("ArtistGetAsync",
                                  new
                                  {
                                      id = result.Id
                                  },
                                  result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ArtistOutputModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] ArtistInputModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _artistsService.UpdateAsync(id, value);

            if (result == null)
            {
                return BadRequest($"Update of artist {id} failed");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _artistsService.DeleteAsync(id);

            return NoContent();
        }
    }
}
