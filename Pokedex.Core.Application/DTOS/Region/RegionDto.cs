using Pokedex.Core.Application.DTOS.Pokemon;
using Pokedex.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Core.Application.DTOS.Region
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PokemonDto> Pokemons { get; set; }
    }
}
