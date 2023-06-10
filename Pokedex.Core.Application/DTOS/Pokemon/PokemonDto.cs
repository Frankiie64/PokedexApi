using Pokedex.Core.Application.DTOS.Region;
using Pokedex.Core.Application.DTOS.TypePokemon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Core.Application.DTOS.Pokemon
{
    public class PokemonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlPhoto { get; set; }
        public Guid RegionId { get; set; }
        public RegionDto Region { get; set; }
        public Guid TypeId { get; set; }
        public TypePokemonDto TypePokemon { get; set; }
    }
}
