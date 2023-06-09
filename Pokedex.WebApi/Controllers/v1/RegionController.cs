using Microsoft.AspNetCore.Mvc;
using Pokedex.Core.Application.Interfaces.Repositories;
using Pokedex.Core.Domain.Entities;
using Pokedex.Infrastructure.Persistence.Repositories;

namespace Pokedex.WebApi.Controllers.v1
{
    [Route("api/region")]
    [ApiVersion("1.0")]
    public class RegionController : BaseController
    {
        private IGenericRepository<Region> _repo;

        public RegionController()
        {
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _repo.GetList(
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
