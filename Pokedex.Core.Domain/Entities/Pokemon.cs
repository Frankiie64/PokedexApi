using Pokedex.Core.Domain.Commons;

namespace Pokedex.Core.Domain.Entities
{
    public class Pokemon : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string UrlPhoto { get; set; }
        public Guid RegionId { get; set; }
        public virtual Region Region { get; set; }
        public Guid TypeId { get; set; }
        public virtual TypePokemon TypePokemon { get; set; }

    }
}