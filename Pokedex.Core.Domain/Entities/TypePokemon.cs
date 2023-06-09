﻿using Pokedex.Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Core.Domain.Entities
{
    public class TypePokemon : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlPhoto { get; set; } 
        public ICollection<Pokemon> Pokemons { get; set; }
    }
}