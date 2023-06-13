using AutoMapper;
using Pokedex.Core.Application.DTOS.Pokemon;
using Pokedex.Core.Application.DTOS.Region;
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
              .ForMember(dest => dest.UrlPhoto, opt => opt.MapFrom(src => src.GetUrl()))
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.getId()))
               .ReverseMap()
               .ForMember(x => x.File, opt => opt.Ignore());

            CreateMap<TypePokemonDto, TypePokemon>()
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreateBy, opt => opt.Ignore())
               .ForMember(x => x.LastUpdated, opt => opt.Ignore())
               .ForMember(x => x.LastUpdatedBy, opt => opt.Ignore())              
               .ReverseMap();

            CreateMap<TypePokemonDto, SaveTypePokemonDto>()
               .ReverseMap()
               .ForMember(x => x.Pokemons, opt => opt.Ignore());

            CreateMap<SaveRegionDto, Region>()
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreateBy, opt => opt.Ignore())
               .ForMember(x => x.LastUpdated, opt => opt.Ignore())
               .ForMember(x => x.LastUpdatedBy, opt => opt.Ignore())
               .ForMember(x => x.Pokemons, opt => opt.Ignore())
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.getId()))
               .ReverseMap();

            CreateMap<RegionDto, Region>()
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreateBy, opt => opt.Ignore())
               .ForMember(x => x.LastUpdated, opt => opt.Ignore())
               .ForMember(x => x.LastUpdatedBy, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<RegionDto, SaveRegionDto>()
               .ReverseMap()
               .ForMember(x => x.Pokemons, opt => opt.Ignore());

            CreateMap<SavePokemonDto, Pokemon>()
              .ForMember(x => x.Created, opt => opt.Ignore())
              .ForMember(x => x.CreateBy, opt => opt.Ignore())
              .ForMember(x => x.LastUpdated, opt => opt.Ignore())
              .ForMember(x => x.LastUpdatedBy, opt => opt.Ignore())
              .ForMember(x => x.TypePokemon, opt => opt.Ignore())
              .ForMember(x => x.Region, opt => opt.Ignore())
              .ForMember(dest => dest.UrlPhoto, opt => opt.MapFrom(src => src.GetUrl()))
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.getId()))
              .ReverseMap()
              .ForMember(x => x.File, opt => opt.Ignore());

            CreateMap<PokemonDto, Pokemon>()
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreateBy, opt => opt.Ignore())
               .ForMember(x => x.LastUpdated, opt => opt.Ignore())
               .ForMember(x => x.LastUpdatedBy, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<PokemonDto, SavePokemonDto>()
               .ReverseMap()
               .ForMember(x => x.TypePokemon, opt => opt.Ignore())
                .ForMember(x => x.Region, opt => opt.Ignore());
        }
    }
}
