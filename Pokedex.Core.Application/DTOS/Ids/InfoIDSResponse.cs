namespace Pokedex.Core.Application.DTOS.Ids
{
    public class InfoIDSResponse
    {
        public bool HasError { get; set; } = false;
        public string? Message { get; set; }
        public string? Technicalfailure { get; set; }
    }
}
