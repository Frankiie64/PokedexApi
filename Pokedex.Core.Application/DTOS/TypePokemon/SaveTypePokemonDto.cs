using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Pokedex.Core.Application.DTOS.TypePokemon
{
    public class SaveTypePokemonDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Debes ingresar un nombre para este tipo de pokemon")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Debes ingresar una descripcion para este tipo de pokemon")]
        public string Description { get; set; }
        private string UrlPhoto { get; set; }
        [Required(ErrorMessage = "Debes ingresar el logo de este tipo de pokemon")]
        public IFormFile File { get; set; }

        public void SetUrl(string url)
        {
            this.UrlPhoto = url;
        }

        public string GetUrl()
        {
            return this.UrlPhoto;
        }

    }
}
