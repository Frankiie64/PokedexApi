﻿using Pokedex.Core.Application.DTOS.Pokemon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Core.Application.DTOS.TypePokemon
{
    public class TypePokemonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlPhoto { get; set; }
        public ICollection<PokemonDto> Pokemons { get; set; }
    }
}
