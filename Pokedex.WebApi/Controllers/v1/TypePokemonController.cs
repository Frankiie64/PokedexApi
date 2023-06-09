using Microsoft.AspNetCore.Mvc;
using Pokedex.Core.Application.DTOS.TypePokemon;
using Pokedex.Core.Application.Interfaces.Services;
using Pokedex.Core.Domain.Entities;

namespace Pokedex.WebApi.Controllers.v1
{
    [Route("api/typePokemon")]
    [ApiVersion("1.0")]
    public class TypePokemonController : BaseController
    {
        private IGenericService<SaveTypePokemonDto, TypePokemonDto, TypePokemon> _service;

        public TypePokemonController(IGenericService<SaveTypePokemonDto, TypePokemonDto, TypePokemon> service)
        {
            _service = service;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetList(
                    include: x => x.Pokemons,
                    predicate: null
                );
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al recuperar datos de la base de datos.");
            }
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _service.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm]SaveTypePokemonDto sv)
        {
            try
            {                
                var response = await _service.Exists(x => x.Id == sv.Id);
                if (response)
                {
                    return BadRequest("El tipo de pokemon ya existe.");
                }

                return Ok(await _service.Add(sv));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(SaveTypePokemonDto sv)
        {
            try
            {
                var response = await _service.Exists(x => x.Id == sv.Id);

                if (!response)
                {
                    return BadRequest("El tipo de pokemon no existe.");
                }

                return Ok(await _service.Update(sv));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                var response = await _service.Exists(x => x.Id == Id);
                if (!response)
                    return Ok("El tipo de pokemon no existe");

                return Ok(await _service.Delete(Id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }


        [HttpGet("Search")]
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
