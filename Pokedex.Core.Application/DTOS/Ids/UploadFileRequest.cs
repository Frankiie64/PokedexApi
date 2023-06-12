using Microsoft.AspNetCore.Http;

namespace Pokedex.Core.Application.DTOS.Ids
{
    public class UploadFileRequest
    {
        public IFormFile file { get; set; }
        public string Id { get; set; }
        public bool editMode { get; set; } = false;
        public string path { get; set; } = "";
    }
}
