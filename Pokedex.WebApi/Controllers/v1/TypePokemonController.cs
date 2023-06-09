using Microsoft.AspNetCore.Mvc;
using Pokedex.Core.Application.DTOS.TypePokemon;
using Pokedex.Core.Application.Interfaces.Repositories;
using Pokedex.Core.Application.Interfaces.Services;
using Pokedex.Core.Domain.Entities;
using Pokedex.Infrastructure.Persistence.Repositories;

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
    }
}
