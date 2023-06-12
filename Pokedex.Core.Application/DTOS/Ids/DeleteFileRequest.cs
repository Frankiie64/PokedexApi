namespace Pokedex.Core.Application.DTOS.Ids
{
    public class DeleteFileRequest
    {
        public string Route { get; set; }
        public string Owner { get; set; }
        public string Id { get; set; }
    }
}
