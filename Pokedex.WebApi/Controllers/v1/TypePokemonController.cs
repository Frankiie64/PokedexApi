using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pokedex.Core.Application.DTOS.TypePokemon;
using Pokedex.Core.Application.Interfaces.Services;
using Pokedex.Core.Domain.Entities;

namespace Pokedex.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
        public class TypePokemonController : BaseController
    {
        private IGenericService<SaveTypePokemonDto, TypePokemonDto, TypePokemon> _service;

        public TypePokemonController(IGenericService<SaveTypePokemonDto, TypePokemonDto, TypePokemon> service)
        {
            _service = service;
        }

        /// <summary>
        /// Devuelve todos los tipos de pokémon.
        /// </summary>
        /// <returns>Una lista de objetos TypePokemonDto.</returns>
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(IEnumerable<TypePokemonDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetList(
                    include: x => x.Include(y=>y.Pokemons),
                    predicate: null
                );

                if (result.Count() == 0)
                {
                    return NotFound("No se han encontrado tipo de pokemones.");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al recuperar datos de la base de datos.");
            }
        }
        /// <summary>
        /// // Devuelve un tipo de Pokémon por su identificador.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un objeto TypePokemonDto. </returns>

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
                    return NotFound("El tipo de pokemon no existe.");
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
        /// Agrega un nuevo tipo de Pokémon.
        /// </summary>
        /// <param name="sv"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromForm] SaveTypePokemonDto sv)
        {
            try
            {
                var response = await _service.Exists(x => x.Id == sv.Id);
                if (response)
                {
                    return BadRequest("El tipo de pokemon ya existe.");
                }

                sv.setUrl("Prueba de url");

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
        /// Actualiza un tipo de Pokémon existente.
        /// </summary>
        /// <param name="sv"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromForm] SaveTypePokemonDto sv)
        {
            try
            {
                var response = await _service.Exists(x => x.Id == sv.Id);

                if (!response)
                {
                    return NotFound("El tipo de pokemon no existe.");
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
        /// Elimina un tipo de Pokémon por su identificador.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Una confirmación segun sea el caso </returns>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromQuery] Guid Id)
        {
            try
            {
                var response = await _service.Exists(x => x.Id == Id);
                if (!response)
                    return BadRequest("El tipo de pokemon no existe");

                if (await _service.Delete(Id))
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
        /// Busca tipos de Pokémon por nombre.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Una lista de objetos TypePokemonDto.</returns>
        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    BadRequest(name);
                }

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
