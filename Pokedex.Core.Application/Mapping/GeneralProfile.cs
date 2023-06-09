using AutoMapper;
using Pokedex.Core.Application.DTOS.TypePokemon;
using Pokedex.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<SaveTypePokemonDto, TypePokemon>()
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreateBy, opt => opt.Ignore())
               .ForMember(x => x.LastUpdated, opt => opt.Ignore())
               .ForMember(x => x.LastUpdatedBy, opt => opt.Ignore())
               .ForMember(x => x.Pokemons, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(x => x.File, opt => opt.Ignore());

            CreateMap<TypePokemonDto, TypePokemon>()
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreateBy, opt => opt.Ignore())
               .ForMember(x => x.LastUpdated, opt => opt.Ignore())
               .ForMember(x => x.LastUpdatedBy, opt => opt.Ignore())
               .ForMember(x => x.Pokemons, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<TypePokemonDto, SaveTypePokemonDto>()
               .ReverseMap();
        }
    }
}
