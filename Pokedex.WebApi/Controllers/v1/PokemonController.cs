using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pokedex.Core.Application.DTOS.Pokemon;
using Pokedex.Core.Application.Interfaces.Services;
using Pokedex.Core.Domain.Entities;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using Pokedex.Core.Application.DTOS.Ids;
using Pokedex.Infrastructure.Share.Services;

namespace Pokedex.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PokemonController : BaseController
    {
        private readonly IGenericService<SavePokemonDto, PokemonDto, Pokemon> _service;
        private readonly IIdsService _IdsService;
        public PokemonController(IGenericService<SavePokemonDto, PokemonDto, Pokemon> service, IIdsService idsService)
        {
            _service = service;
            _IdsService = idsService;
        }

        /// <summary>
        /// Devuelve todas los pokemones.
        /// </summary>
        /// <returns>Una lista de objetos PokemonDto.</returns>
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(IEnumerable<PokemonDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetList(
                            include: query => query.Include(x => x.TypePokemon)
                           .Include(x=>x.Region), 
                            predicate: null
                );

                if (result.Count() == 0)
                {
                    return NotFound("No se han encontrado pokemones.");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
        /// <summary>
        /// Devuelve un pokemon por su identificador.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un objeto PokemonDto segun su identificador.</returns>

        [HttpGet("getById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var response = await _service.Exists(
                    predicate: x => x.Id == id
                    );

                if (!response)
                {
                    return NotFound("El pokemon no existe.");
                }

                var result = await _service.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        /// <summary>
        /// Agrega un nuevo Pokemon.
        /// </summary>
        /// <param name="sv"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromForm] SavePokemonDto sv)
        {
            try
            {
                var response = await _service.Exists(x => x.Id == sv.Id);
                if (response)
                {
                    return BadRequest("El pokemon ya existe.");
                }

                var responseFromIds = await _IdsService.UploadFile(new UploadFileRequest
                {
                    Id = sv.Id.ToString(),
                    file = sv.File,
                });

                if (responseFromIds == null || responseFromIds.Info.HasError)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Mesaje :" + responseFromIds.Info.Message.ToString() + " Falla Tecnica : " + responseFromIds.Info.Technicalfailure);
                }

                sv.SetUrl(responseFromIds.Url);
                if (!_service.Add(sv).Result)
                {
                    return BadRequest("No se ha podido completar esta acción.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        /// <summary>
        /// Actualiza un Pokemon existente.
        /// </summary>
        /// <param name="sv"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromForm] SavePokemonDto sv)
        {
            try
            {
                var response = await _service.Exists(x => x.Id == sv.Id);

                if (!response)
                {
                    return NotFound("El pokemon no existe.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Todos los campos son obligatios");
                }

                if (!_service.Update(sv).Result)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un fallo en nuestros servidores");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        /// <summary>
        /// Elimina un Pokemon por su identificador.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Una confirmación segun sea el caso </returns>
        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            try
            {
                var response = await _service.Exists(x => x.Id == id);
                if (!response)
                    return BadRequest("El pokemon no existe");

                if (await _service.Delete(id))
                {
                    return Ok(true);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un fallo tecnico en nuestros servidores.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        /// <summary>
        /// Busca Pokemones por nombre.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Una lista de objetos PokemonDto.</returns>
        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchByName(string name)
        {
            try
            {
                var entity = await _service.FindWhere(
                    predicate: x => x.Name.Contains(name),
                    include: null
                    );
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }
    }
}
