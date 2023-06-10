using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Core.Application.DTOS.Region
{
    public class SaveRegionDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Debes ingresar un nombre para este tipo de pokemon")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Debes ingresar una descripcion para este tipo de pokemon")]
        public string Description { get; set; }
    
    }
}
