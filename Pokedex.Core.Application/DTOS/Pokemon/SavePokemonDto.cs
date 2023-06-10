﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Pokedex.Core.Application.DTOS.Pokemon
{
    public class SavePokemonDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Debes ingresar el nombre del pokemon.")]
        public string Name { get; set; }
        private string UrlPhoto { get; set; }
        [Required(ErrorMessage = "Debes ingresar la region a la que pertenece.")]
        public Guid RegionId { get; set; }
        [Required(ErrorMessage = "Debes ingresar el tipo de pokemon que pertenece.")]
        public Guid TypeId { get; set; }
        [Required(ErrorMessage = "Debes ingresar una foto del pokemon.")]
        public IFormFile File { get; set; }


        public void setUrl(string url)
        {
            this.UrlPhoto = url;  
        }
        public string gettUrl()
        {
            return this.UrlPhoto;
        }
    }
}