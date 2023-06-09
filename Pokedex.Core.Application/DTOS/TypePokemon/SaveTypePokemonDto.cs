using Microsoft.AspNetCore.Http;

namespace Pokedex.Core.Application.DTOS.TypePokemon
{
    public class SaveTypePokemonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlPhoto { get; set; }
        public IFormFile File { get; set; }

      
    }
}
