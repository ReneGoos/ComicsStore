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
    public class CharactersController : ControllerBase
    {
        private readonly IComicsStoreService<CharacterInputModel, CharacterOutputModel, BasicSearchModel> _charactersService;

        public CharactersController(IComicsStoreService<CharacterInputModel, CharacterOutputModel, BasicSearchModel> charactersService)
        {
            _charactersService = charactersService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CharacterOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromQuery] BasicSearchModel characterSearchModel)
        {
            return Ok(await _charactersService.GetAsync(characterSearchModel));
        }

        [HttpGet("{id}", Name = "CharacterGetAsync")]
        [ProducesResponseType(typeof(CharacterOutputModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var showModel = await _charactersService.GetAsync(id);

            if (showModel == null)
            {
                return NotFound();
            }

            return Ok(showModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CharacterOutputModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] CharacterInputModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _charactersService.AddAsync(value);

            if (result == null)
            {
                return BadRequest("Character not inserted");
            }

            return CreatedAtRoute("CharacterGetAsync",
                                  new
                                  {
                                      id = result.Id
                                  },
                                  result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CharacterOutputModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] CharacterInputModel value)
        {
            if (value == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await _charactersService.UpdateAsync(id, value);

            if (result == null)
            {
                return BadRequest($"Update of character {id} failed");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _charactersService.DeleteAsync(id);

            return NoContent();
        }
    }
}