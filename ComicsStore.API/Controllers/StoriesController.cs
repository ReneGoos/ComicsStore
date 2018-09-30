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
    public class StoriesController : ControllerBase
    {
        private readonly IStoriesService _storiesService;

        public StoriesController(IStoriesService storiesService)
        {
            _storiesService = storiesService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<StoryOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromQuery] StorySearchModel storySearchModel)
        {
            return Ok(await _storiesService.GetAsync(storySearchModel));
        }

        [HttpGet("{id}", Name = "StoryGetAsync")]
        [ProducesResponseType(typeof(StoryOutputModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var showModel = await _storiesService.GetAsync(id);

            if (showModel == null)
            {
                return NotFound();
            }

            return Ok(showModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(StoryOutputModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] StoryInputModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _storiesService.AddAsync(value);

            if (result == null)
            {
                return BadRequest("Story not inserted");
            }

            return CreatedAtRoute("StoryGetAsync",
                                  new
                                  {
                                      id = result.Id
                                  },
                                  result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(StoryOutputModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] StoryInputModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _storiesService.UpdateAsync(id, value);

            if (result == null)
            {
                return BadRequest($"Update of story {id} failed");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _storiesService.DeleteAsync(id);

            return NoContent();
        }
    }
}